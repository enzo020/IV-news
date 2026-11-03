using IVnews.DTOs;
using IVnews.DTOs.ApiTube;
using IVnews.Model;

namespace IVNews.Services
{
    public interface INoticiaService
    {
        Task<List<ApiTubeArticle>> ObterNoticiasDaApiAsync(int perPage);
        Task<List<ApiTubeArticle>> ObterNoticiasDaApiAsync(
            int perPage,
            int categoriaBancoId);

        Task<ImportacaoResultadoDto> SalvarNoticiasDaApiAsync(
            int perPage,
            int categoriaBancoId);
        Task<ImportacaoResultadoDto> SalvarNoticiasDaApiAsync(int perPage);
        Task<Localizacao> SalvarLocalizacaoAsync(LocalizacaoDto localizacaoDto);
        Task<Categoria> SalvarCategoriaAsync(CategoriaDto categoriaDto);
        Task GerarESalvarImagemDaNoticiaAsync(Noticia noticia, string? prompt);
    }
}