using IVnews.DTOs;
using IVnews.DTOs.ApiTube;
using IVnews.Model;

namespace IVNews.Services
{
    public interface INoticiaService
    {
        Task<List<ApiTubeArticle>> ObterNoticiasDaApiAsync(int perPage);
        Task<ImportacaoResultadoDto> SalvarNoticiasDaApiAsync(int perPage);
        Task<Localizacao> SalvarLocalizacaoAsync(LocalizacaoDto localizacaoDto);
    }
}