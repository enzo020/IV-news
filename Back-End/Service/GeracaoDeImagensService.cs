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

    public async Task<string> GerarImagemAsync(string prompt)
    {
        var defaultUrl = $"https://api.cloudflare.com/client/v4/accounts/{_configuration.GetValue<string>("Cloudflare:AccountId")}{_configuration.GetValue<string>("Cloudflare:Model")}";

        var token = _configuration.GetValue<string>("Cloudflare:ApiToken");

        int width = 512, height = 512;

        using var requestBody = new MultipartFormDataContent
        {
            { new StringContent(prompt), "prompt" },
            { new StringContent(width.ToString()), "width" },
            { new StringContent(height.ToString()), "height" }
        };

        using var request = new HttpRequestMessage(HttpMethod.Post, defaultUrl)
        {
            Content = requestBody
        };

        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        Console.WriteLine($"Token nulo: {token == null}");
        Console.WriteLine($"Token vazio: {string.IsNullOrWhiteSpace(token)}");
        Console.WriteLine($"Token começa com Bearer: {token?.StartsWith("Bearer ") == true}");
        Console.WriteLine($"Tamanho do token: {token?.Length}");
        var response = await _httpclient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var erro = await response.Content.ReadAsStringAsync();
            throw new Exception($"Erro ao gerar imagem: {erro}");
        }

        var responseContent = await response.Content.ReadAsStringAsync();

        using var jsonResponse = System.Text.Json.JsonDocument.Parse(responseContent);

        var imagemBase64 = jsonResponse.RootElement
            .GetProperty("result")
            .GetProperty("image")
            .GetString();

        return imagemBase64 ?? throw new Exception("Erro ao gerar imagem: resposta inválida do servidor.");
    }
}