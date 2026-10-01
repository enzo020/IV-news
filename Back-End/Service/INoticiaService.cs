using IVnews.DTOs.ApiTube;

namespace IVNews.Services
{
    public interface INoticiaService
    {
        Task<List<ApiTubeArticle>> ObterNoticiasDaApiAsync(int perPage);
        Task<ImportacaoResultadoDto> SalvarNoticiasDaApiAsync(int perPage);

    }
}