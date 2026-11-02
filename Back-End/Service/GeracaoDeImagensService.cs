using System.Net.Http.Headers;
using System.Text.Json;
using IVnews.Data;

public class GeracaoDeImagensService : IGeracaoDeImagensService
{
    private readonly HttpClient _httpclient;
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public GeracaoDeImagensService(HttpClient httpclient, AppDbContext context, IConfiguration configuration)
    {
        _httpclient = httpclient;
        _context = context;
        _configuration = configuration;

    }

    public async Task<byte[]> GerarImagemAsync(string prompt)
    {
        var accountId = _configuration["Cloudflare:AccountId"];
        var apiToken = _configuration["Cloudflare:ApiToken"];
        string width = "512", height = "512";

        if (string.IsNullOrWhiteSpace(accountId) ||
            string.IsNullOrWhiteSpace(apiToken))
        {
            throw new InvalidOperationException(
                "As credenciais da Cloudflare não foram configuradas."
            );
        }

        var endpoint = $"https://api.cloudflare.com/client/v4/accounts/{_configuration.GetValue<string>("Cloudflare:AccountId")}{_configuration.GetValue<string>("Cloudflare:Model")}";

        using var form = new MultipartFormDataContent();

        form.Add(new StringContent(prompt), "prompt");
        form.Add(new StringContent(width), "width");
        form.Add(new StringContent(height), "height");

        using var request = new HttpRequestMessage(HttpMethod.Post, endpoint);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);

        request.Content = form;

        using var response = await _httpclient.SendAsync(request);

        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Erro na geração da imagem: {responseContent}"
            );
        }

        using var json = JsonDocument.Parse(responseContent);

        if (!json.RootElement.TryGetProperty("result", out var result) ||
            !result.TryGetProperty("image", out var imageElement))
        {
            throw new InvalidOperationException(
                "A resposta da Cloudflare não contém a imagem esperada."
            );
        }

        var imagemBase64 = imageElement.GetString();

        if (string.IsNullOrWhiteSpace(imagemBase64))
        {
            throw new InvalidOperationException(
                "A Cloudflare retornou uma imagem vazia."
            );
        }

        return Convert.FromBase64String(imagemBase64);
    }
}
