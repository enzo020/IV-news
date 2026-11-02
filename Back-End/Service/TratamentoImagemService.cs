using SixLabors.ImageSharp;

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

    public static bool PareceBanner(int largura, int altura)
    {
        if (altura == 0)
            return true;

        var proporcao = (double)largura / altura;

        return proporcao > 3.2 || proporcao < 0.45;
    }

    public async Task<long?> ObterPontuacaoImagemAsync(string url)
    {
        try
        {
            using var response = await _httpClient.GetAsync(
                url,
                HttpCompletionOption.ResponseHeadersRead
            );

            if (!response.IsSuccessStatusCode)
                return null;

            var contentType = response.Content.Headers.ContentType?.MediaType;

            if (contentType == null ||
                !contentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
                return null;

            // Evita baixar arquivos excessivamente grandes.
            if (response.Content.Headers.ContentLength > 20_000_000)
                return null;

            await using var stream = await response.Content.ReadAsStreamAsync();

            var info = await Image.IdentifyAsync(stream);

            if (info == null)
                return null;

            int largura = info.Width;
            int altura = info.Height;

            if (PareceBanner(largura, altura))
                return null;

            long quantidadePixels = (long)largura * altura;

            // Rejeita imagens pequenas demais.
            if (Math.Max(largura, altura) < 700 ||
                quantidadePixels < 350_000)
                return null;

            // A resolução determina a pontuação.
            return quantidadePixels;
        }
        catch
        {
            return null;
        }
    }

}