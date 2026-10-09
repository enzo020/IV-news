using SmartReader;

namespace IVNews.Backend.Services;

public class ContentExtractorService
{
    public async Task<ExtractionResult> ExtractAsync(string url)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) ||
            (uri.Scheme != Uri.UriSchemeHttp &&
             uri.Scheme != Uri.UriSchemeHttps))
        {
            return new ExtractionResult(
                false, false, "", "URL inválida");
        }

        try
        {
            var reader = new Reader(uri.ToString());
            var article = await reader.GetArticleAsync();

            var text = article.TextContent ?? "";

            return new ExtractionResult(
                article.IsReadable,
                article.Completed,
                text,
                article.Errors.FirstOrDefault()?.Message
            );
        }
        catch (Exception ex)
        {
            return new ExtractionResult(
                false, false, "", ex.Message);
        }
    }
}

public record ExtractionResult(
    bool IsReadable,
    bool Completed,
    string Text,
    string? Error
);