
using System.Net.Http.Headers;
using System.Text.Json;

public class ImagemService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public ImagemService(
        HttpClient httpClient,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<byte[]> GerarImagemAsync(string descricao)
    {
        var accountId = _configuration["Cloudflare:AccountId"];
        var apiToken = _configuration["Cloudflare:ApiToken"];

        if (string.IsNullOrWhiteSpace(accountId) ||
            string.IsNullOrWhiteSpace(apiToken))
        {
            throw new InvalidOperationException(
                "As credenciais da Cloudflare não foram configuradas."
            );
        }

        var endpoint =
            $"https://api.cloudflare.com/client/v4/accounts/{accountId}/ai/run/@cf/black-forest-labs/flux-2-klein-4b";

        var prompt = $"""
            Create a high-quality editorial news photograph
            illustrating the following news:
            {descricao}

            Professional photojournalism, realistic photography,
            no text, no captions, no watermarks.
            """;

        using var form = new MultipartFormDataContent();

        form.Add(new StringContent(prompt), "prompt");
        form.Add(new StringContent("1024"), "width");
        form.Add(new StringContent("768"), "height");

        using var request = new HttpRequestMessage(
            HttpMethod.Post, endpoint);

        request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", apiToken);

        request.Content = form;

        using var response = await _httpClient.SendAsync(request);

        var responseContent =
            await response.Content.ReadAsStringAsync();

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
