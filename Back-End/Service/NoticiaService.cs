using System.Net.Http.Headers;
using System.Text.Json;
using IVnews.Data;
using IVnews.DTOs.ApiTube;
using IVnews.Model;
using Microsoft.EntityFrameworkCore;

namespace IVNews.Services
{
    public class NoticiaService : INoticiaService
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public NoticiaService(HttpClient httpClient, IConfiguration configuration, AppDbContext context)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _context = context;

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
        public async Task<ImportacaoResultadoDto> SalvarNoticiasDaApiAsync(int perPage)
{
    var noticiasApi = await ObterNoticiasDaApiAsync(perPage);
    
    int inseridas = 0;
    int ignoradas = 0;

    foreach (var article in noticiasApi)
    {
        var noticiaExistente = await _context.Noticias
            .FirstOrDefaultAsync(n => n.IdExterno == article.Id.ToString());

        if (noticiaExistente == null)
        {
            var noticia = new Noticia
            {
                Titulo = article.Title,
                Conteudo = article.Body,
                Autor = article.Author?.Name,
                Fonte = article.Source?.Domain,
                UrlNoticia = article.Href,
                ImagemUrl = article.Media?.FirstOrDefault()?.Url,
                IdExterno = article.Id.ToString(),
                PublicadoEm = article.PublishedAt
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