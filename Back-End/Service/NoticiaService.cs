using System.Net.Http.Headers;

namespace IVNews.Services
{
    public class NoticiaService : INoticiaService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public NoticiaService(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

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

        public async Task<string> ObterNoticiasDaApiAsync()
        {
            var endpoint =
                "/v1/news/everything?language.code=pt&per_page=5";

            var response = await _httpClient.GetAsync(endpoint);

            var jsonResponse =
                await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"Erro ao consumir a APITube. " +
                    $"Status: {(int)response.StatusCode} " +
                    $"({response.StatusCode}). " +
                    $"Resposta: {jsonResponse}"
                );
            }

            return jsonResponse;
        }
    }
}