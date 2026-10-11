namespace IVNews.Backend.Services;

public interface IContentExtractorService
{
    Task<ExtractionResult> ExtractAsync(string url);
}