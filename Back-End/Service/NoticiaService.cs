using IVnews.Data;
using IVnews.Model;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;

namespace IVNews.Services
{
    public class NoticiaService : INoticiaService
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

       public NoticiaService(AppDbContext context, HttpClient httpClient, IConfiguration configuration)
        {
            _context = context;
            _httpClient = httpClient;
            _configuration = configuration;
            
            // Lendo a URL e o Token de forma segura das configurações
            var baseUrl = _configuration["ApiTubeSettings:BaseUrl"];
            var token = _configuration["ApiTubeSettings:Token"];

            _httpClient.BaseAddress = new Uri(baseUrl);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<IEnumerable<Noticia>> ObterNoticiasDaApiAsync()
        {
            // Faz a chamada GET para a API TUBE
            var response = await _httpClient.GetAsync("v1/noticias"); // Endpoint de exemplo da API TUBE
            
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Erro ao consumir a API TUBE.");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            
            // Desserializa o JSON para a sua model (ajuste conforme o formato de retorno da API TUBE)
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var noticiasDaApi = JsonSerializer.Deserialize<IEnumerable<Noticia>>(jsonResponse, options);

            return noticiasDaApi ?? new List<Noticia>();
        }

        public async Task SincronizarNoticiasAsync()
        {
            var noticias = await ObterNoticiasDaApiAsync();

            // Opcional: Lógica para salvar ou atualizar no banco de dados local (_context)
            foreach (var noticia in noticias)
            {
                bool existe = await _context.Noticias.AnyAsync(n => n.Id == noticia.Id);
                if (!existe)
                {
                    _context.Noticias.Add(noticia);
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}