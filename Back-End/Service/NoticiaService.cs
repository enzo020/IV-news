using System.Net.Http.Headers;
using System.Text.Json;
using IVnews.Data;
using IVnews.DTOs.ApiTube;
using IVnews.Model;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;

namespace IVNews.Services
{
    public class NoticiaService : INoticiaService
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ImagemService _imagemService;

        public NoticiaService(HttpClient httpClient, IConfiguration configuration, AppDbContext context, ImagemService imagemService)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _context = context;
            _imagemService = imagemService;

            var baseUrl = _configuration["ApiTubeSettings:BaseUrl"];
            var token = _configuration["ApiTubeSettings:Token"];

            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new InvalidOperationException(
                    "ApiTubeSettings:BaseUrl não foi configurado."
                );
            }

            if (string.IsNullOrWhiteSpace(token))
            {
                throw new InvalidOperationException(
                    "ApiTubeSettings:Token não foi configurado."
                );
            }

            _httpClient.BaseAddress = new Uri(baseUrl);

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        // Mapeamento das categorias ApiTube > IvNews
        private static readonly Dictionary<string, int> CategoriaMapeamento = new()
        {
            // Política
            ["medtop:11000000"] = 1,
            ["medtop:20000574"] = 1,
            ["medtop:20000586"] = 1,

            // Economia
            ["medtop:04000000"] = 5,
            ["medtop:20000170"] = 5,
            ["medtop:20000209"] = 5,
            ["medtop:20000200"] = 5,
            ["medtop:20001366"] = 5,

            // Esporte
            ["medtop:15000000"] = 2,

            // Tecnologia
            // adicionar categorias de tecnologia aqui

            // Saúde
            // adicionar categorias de saúde aqui
        };

        public async Task<List<ApiTubeArticle>> ObterNoticiasDaApiAsync(int perPage)
        {
            var endpoint = $"v1/news/everything?language.code=pt&per_page={perPage}&source.country.code=br";

            HttpResponseMessage response;

            try
            {
                // Tenta realizar a requisição à API externa
                response = await _httpClient.GetAsync(endpoint);
            }
            catch (TaskCanceledException)
            {
                throw new HttpRequestException(
                    "[ApiTube Error] A requisição excedeu o tempo limite (Timeout). O serviço do ApiTube pode estar indisponível ou instável."
                );
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(
                    $"[ApiTube Error] Falha de conexão de rede ao tentar acessar o ApiTube. Verifique sua conexão com a internet. Detalhes: {ex.Message}",
                    ex
                );
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();

            Console.WriteLine(jsonResponse);

            // Se a API retornar erro HTTP (ex: 502, 503, 504, 500)
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"[ApiTube Error] O ApiTube retornou um erro. Status Code: {(int)response.StatusCode} ({response.StatusCode}). " +
                    $"Detalhes: {jsonResponse}"
                );
            }

            if (string.IsNullOrWhiteSpace(jsonResponse))
            {
                throw new InvalidOperationException(
                    "[ApiTube Error] A API retornou uma resposta com corpo vazio."
                );
            }

            var data = JsonSerializer.Deserialize<ApiTubeResponse>(
                jsonResponse,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );

            if (data?.Results == null)
            {
                throw new InvalidOperationException(
                    $"[ApiTube Error] Falha ao processar os dados recebidos. JSON: {jsonResponse}"
                );
            }

            return data.Results;
        }

        private static string? ObterIdExternoCategoria(ApiTubeCategory categoria)
        {
            return categoria.Links?.Self?
                .Split('/', StringSplitOptions.RemoveEmptyEntries)
                .LastOrDefault();
        }

        private static Categoria? ObterCategoria(ApiTubeArticle article, List<Categoria> categoriasBanco)
        {
            const int CategoriaOutrosId = 6;
            var categorias = article.Categories?
                .OrderByDescending(c => c.Score)
                ?? Enumerable.Empty<ApiTubeCategory>();

            foreach (var categoriaApi in categorias)
            {
                var idExterno = ObterIdExternoCategoria(categoriaApi);

                if (idExterno != null && CategoriaMapeamento.TryGetValue(idExterno, out var categoriaId))
                    return categoriasBanco.FirstOrDefault(c => c.Id == categoriaId);


            }
            return categoriasBanco.FirstOrDefault(c => c.Id == CategoriaOutrosId);
        }

        private async Task<string?> ObterImagemValidaAsync(ApiTubeArticle article)
        {
            if (article.Media == null)
                return null;

            foreach (var media in article.Media)
            {
                if (string.IsNullOrWhiteSpace(media.Url))
                    continue;

                if (await _imagemService.ImagemEhValida(media.Url))
                    return media.Url;
            }

            return null;
        }

        public async Task<ImportacaoResultadoDto> SalvarNoticiasDaApiAsync(int perPage)
        {
            var noticiasApi = await ObterNoticiasDaApiAsync(perPage);

            int inseridas = 0;
            int ignoradas = 0;

            var categoriasBanco = await _context.Categorias.ToListAsync();

            foreach (var article in noticiasApi)
            {
                var noticiaExistente = await _context.Noticias
                    .FirstOrDefaultAsync(n => n.IdExterno == article.Id.ToString());

                if (noticiaExistente == null)
                {
                    var categoria = ObterCategoria(article, categoriasBanco);
                    var imagemUrl = await ObterImagemValidaAsync(article);

                    var noticia = new Noticia
                    {
                        Titulo = article.Title,
                        Conteudo = article.Body,
                        Autor = article.Author?.Name,
                        Fonte = article.Source?.Domain,
                        UrlNoticia = article.Href,
                        ImagemUrl = imagemUrl,
                        IdExterno = article.Id.ToString(),
                        PublicadoEm = article.PublishedAt,
                        Categoria = categoria
                    };

                    _context.Noticias.Add(noticia);
                    inseridas++;
                }
                else
                {
                    ignoradas++;
                }
            }

            if (inseridas > 0)
            {
                await _context.SaveChangesAsync();
            }

            return new ImportacaoResultadoDto
            {
                TotalRecebidoDaApi = noticiasApi.Count,
                TotalInserido = inseridas,
                TotalIgnoradasDuplicadas = ignoradas,
                Mensagem = $"Importação finalizada. Recebidas: {noticiasApi.Count} | Inseridas: {inseridas} | Ignoradas (já existiam): {ignoradas}"
            };
        }
    }
}