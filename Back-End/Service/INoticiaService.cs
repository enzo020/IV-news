using IVnews.DTOs.ApiTube;

namespace IVNews.Services
{
    public interface INoticiaService
    {
        Task<List<ApiTubeArticle>> ObterNoticiasDaApiAsync();

        Task<int> SalvarNoticiasDaApiAsync();
    }
}