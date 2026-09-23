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

        public async Task<List<ApiTubeArticle>> ObterNoticiasDaApiAsync()
        {
            var endpoint = "v1/news/everything" +
                "?language.code=pt" +
                "&per_page=5" +
                "&source.country.code=br";


            var response = await _httpClient.GetAsync(endpoint);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<ApiTubeResponse>(
                jsonResponse,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );

            return data.Results;
        }

        public async Task<int> SalvarNoticiasDaApiAsync()
        {
            var noticiasApi = await ObterNoticiasDaApiAsync();

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
                }
            }
            await _context.SaveChangesAsync();
            return noticiasApi.Count;
        }
    }
}