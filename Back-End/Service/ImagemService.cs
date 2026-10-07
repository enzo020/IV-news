using SixLabors.ImageSharp;

// Arquivo responsável por tratamentos de resolução das imagens
public class ImagemService
{
    private readonly HttpClient _httpClient;

    public ImagemService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public static bool PareceBanner(int largura, int altura)
    {
        if (altura == 0)
            return true;

        var proporcao = (double)largura / altura;

        return proporcao > 4.0 || proporcao < 0.25; ;
    }

    public async Task<bool> ImagemEhValida(string url)
    {
        try
        {
            using var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return false;

            await using var stream = await response.Content.ReadAsStreamAsync();

            using var image = await Image.LoadAsync(stream);

            return !PareceBanner(image.Width, image.Height);
        }
        catch
        {
            return false;
        }
    }
}