using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;
using IVnews.Data;
using IVnews.DTOs;
using IVnews.DTOs.ApiTube;
using IVnews.Model;
using IVNews.Backend.Services;
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
        private readonly IContentExtractorService _contentExtractor;
        private readonly IGeminiService _geminiService;

        public NoticiaService(HttpClient httpClient, IConfiguration configuration, AppDbContext context, ImagemService imagemService, IContentExtractorService contentExtractor, IGeminiService geminiService)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _context = context;
            _imagemService = imagemService;
            _contentExtractor = contentExtractor;
            _geminiService = geminiService;

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
            var endpoint = $"v1/news/everything?language.code=pt&source.country.code=br&per_page={perPage}";

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

        private static double? ConverterCoordenada(string? valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return null;

            return double.TryParse(
                valor,
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out var coordenada)
                    ? coordenada
                    : null;
        }
        private async Task<Localizacao?> ObterOuCriarLocalizacaoAsync(ApiTubeLocation? dados)
        {
            if (dados == null)
                return null;

            var pais = Normalizar(dados.Country);
            var estado = Normalizar(dados.Region);
            var cidade = Normalizar(dados.City);

            // Não cadastrar uma localização completamente vazia.
            if (pais == null && estado == null && cidade == null)
                return null;

            var existente = await _context.Localizacoes.FirstOrDefaultAsync(l =>
                l.Pais == pais &&
                l.Estado == estado &&
                l.Cidade == cidade);

            if (existente != null)
                return existente;

            var localizacao = new Localizacao
            {
                Pais = pais,
                Estado = estado,
                Cidade = cidade,
                Latitude = ConverterCoordenada(dados.Lat),
                Longitude = ConverterCoordenada(dados.Long)
            };

            _context.Localizacoes.Add(localizacao);

            // Persiste para gerar o Id.
            await _context.SaveChangesAsync();

            return localizacao;
        }

        private static string? Normalizar(string? valor)
        {
            return string.IsNullOrWhiteSpace(valor)
                ? null
                : valor.Trim();
        }

        public async Task<Localizacao> SalvarLocalizacaoAsync(LocalizacaoDto localizacaoDto)
        {
            ArgumentNullException.ThrowIfNull(localizacaoDto);

            var pais = Normalizar(localizacaoDto.Pais);
            var estado = Normalizar(localizacaoDto.Estado);
            var cidade = Normalizar(localizacaoDto.Cidade);

            if (pais == null && estado == null && cidade == null)
            {
                throw new ArgumentException(
                    "Informe pelo menos cidade, estado ou país.",
                    nameof(localizacaoDto));
            }

            if (localizacaoDto.Latitude.HasValue &&
                (!double.IsFinite(localizacaoDto.Latitude.Value) ||
                 localizacaoDto.Latitude is < -90 or > 90))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(localizacaoDto),
                    "A latitude deve estar entre -90 e 90.");
            }

            if (localizacaoDto.Longitude.HasValue &&
                (!double.IsFinite(localizacaoDto.Longitude.Value) ||
                 localizacaoDto.Longitude is < -180 or > 180))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(localizacaoDto),
                    "A longitude deve estar entre -180 e 180.");
            }

            var existente = await _context.Localizacoes.FirstOrDefaultAsync(l =>
                l.Pais == pais &&
                l.Estado == estado &&
                l.Cidade == cidade);

            if (existente != null)
                return existente;

            var localizacao = new Localizacao
            {
                Pais = pais,
                Estado = estado,
                Cidade = cidade,
                Latitude = localizacaoDto.Latitude,
                Longitude = localizacaoDto.Longitude
            };

            _context.Localizacoes.Add(localizacao);
            await _context.SaveChangesAsync();

            return localizacao;
        }

        public async Task<ImportacaoResultadoDto> SalvarNoticiasDaApiAsync(int perPage)
        {
            var noticiasApi = await ObterNoticiasDaApiAsync(perPage);

            int inseridas = 0;
            int ignoradas = 0;
            int falhasExtracao = 0;
            int falhasProcessamento = 0;

            var categoriasBanco = await _context.Categorias.ToListAsync();

            foreach (var article in noticiasApi)
            {
                var noticiaExistente = await _context.Noticias
                    .FirstOrDefaultAsync(
                        n => n.IdExterno == article.Id.ToString());

                if (noticiaExistente != null)
                {
                    ignoradas++;
                    continue;
                }

                // 1. Extrai o conteúdo da página original.
                var extraction = await _contentExtractor.ExtractAsync(
                    article.Href ?? "");

                if (string.IsNullOrWhiteSpace(extraction.Text))
                {
                    falhasExtracao++;
                    continue;
                }

                // 2. Processa o texto extraído com o Gemini.
                ProcessamentoNoticiaDto? processada;

                try
                {
                    processada = await _geminiService.ProcessarNoticiaAsync(
                        extraction.Text);
                }
                catch (Exception ex) when (
                    ex is JsonException ||
                    ex is InvalidOperationException)
                {
                    falhasProcessamento++;
                    continue;
                }

                if (processada == null ||
                    string.IsNullOrWhiteSpace(processada.ConteudoTratado) ||
                    string.IsNullOrWhiteSpace(processada.ResumoExecutivo))
                {
                    falhasProcessamento++;
                    continue;
                }

                // 3. Recupera os metadados da notícia.
                var categoria = ObterCategoria(article, categoriasBanco);
                var imagemUrl = await ObterImagemValidaAsync(article);
                var localizacao =
                    await ObterOuCriarLocalizacaoAsync(article.Location);

                // 4. Salva o texto tratado, não o Body do ApiTube.
                var noticia = new Noticia
                {
                    Titulo = article.Title,

                    Conteudo = processada.ConteudoTratado,

                    Autor = article.Author?.Name,
                    Fonte = article.Source?.Domain,
                    UrlNoticia = article.Href,
                    ImagemUrl = imagemUrl,
                    IdExterno = article.Id.ToString(),
                    PublicadoEm = article.PublishedAt,
                    Categoria = categoria,
                    Localizacao = localizacao
                };

                // 5. Cria o resumo associado à notícia.
                var resumo = new Resumo
                {
                    Titulo = noticia.Titulo,
                    ResumoExecutivo = processada.ResumoExecutivo,
                    PontosChave = processada.PontosChave ?? new(),
                    Sentimento = processada.Sentimento,

                    Noticia = noticia
                };

                _context.Noticias.Add(noticia);
                _context.Resumos.Add(resumo);

                inseridas++;
            }

            await _context.SaveChangesAsync();

            return new ImportacaoResultadoDto
            {
                TotalRecebidoDaApi = noticiasApi.Count,
                TotalInserido = inseridas,
                TotalIgnoradasDuplicadas = ignoradas,

                // Adapte esses campos à definição real do seu DTO.
                Mensagem =
                    $"Importação finalizada. " +
                    $"Recebidas: {noticiasApi.Count} | " +
                    $"Inseridas: {inseridas} | " +
                    $"Duplicadas: {ignoradas} | " +
                    $"Falhas na extração: {falhasExtracao} | " +
                    $"Falhas no Gemini: {falhasProcessamento}"
            };
        }
    }
}