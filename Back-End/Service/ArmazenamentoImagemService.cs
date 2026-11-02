using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace IVNews.Backend.Services;

public class ArmazenamentoImagemService
{
    private readonly Cloudinary _cloudinary;

    public ArmazenamentoImagemService(IConfiguration configuration)
    {
        var cloudName = configuration["Cloudinary:CloudName"];
        var apiKey = configuration["Cloudinary:ApiKey"];
        var apiSecret = configuration["Cloudinary:ApiSecret"];

        if (string.IsNullOrWhiteSpace(cloudName) ||
            string.IsNullOrWhiteSpace(apiKey) ||
            string.IsNullOrWhiteSpace(apiSecret))
        {
            throw new InvalidOperationException(
                "As credenciais do Cloudinary não foram configuradas."
            );
        }

        var account = new Account(cloudName, apiKey, apiSecret);

        _cloudinary = new Cloudinary(account);
    }

    public async Task<string> SalvarImagemAsync(
        byte[] imagemBytes,
        string nomeArquivo)
    {
        if (imagemBytes == null || imagemBytes.Length == 0)
            throw new ArgumentException("A imagem está vazia.");

        using var stream = new MemoryStream(imagemBytes);

        var parametros = new ImageUploadParams
        {
            File = new FileDescription(nomeArquivo, stream),
            Folder = "iv-news/noticias"
        };

        var resultado = await _cloudinary.UploadAsync(parametros);

        if (resultado.Error != null || resultado.SecureUrl == null)
        {
            throw new Exception(
                $"Erro ao enviar imagem para o Cloudinary: {resultado.Error?.Message}"
            );
        }

        return resultado.SecureUrl.ToString();
    }
}