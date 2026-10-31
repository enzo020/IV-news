using IVNews.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IVNews.Controllers
{
    [ApiController]
    [Route("api/apitube")]
    [Route("api/noticias/apitube")]
    public class ApiTubeController : ControllerBase
    {
        private readonly INoticiaService _noticiaService;

        public ApiTubeController(INoticiaService noticiaService)
        {
            _noticiaService = noticiaService;
        }

        [HttpGet]
        public async Task<IActionResult> TestarApiTube([FromQuery] int perPage = 5)
        {
            var validacao = ValidarPerPage(perPage);
            if (validacao != null)
                return validacao;

            var noticias = await _noticiaService.ObterNoticiasDaApiAsync(perPage);
            return Ok(noticias);
        }

        [HttpPost("importar")]
        public async Task<IActionResult> ImportarNoticiasDaApiTube([FromQuery] int perPage = 5)
        {
            var validacao = ValidarPerPage(perPage);
            if (validacao != null)
                return validacao;

            try
            {
                var resultado = await _noticiaService.SalvarNoticiasDaApiAsync(perPage);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return CriarRespostaErroImportacao(ex);
            }
        }

        private IActionResult? ValidarPerPage(int perPage)
        {
            if (perPage >= 1 && perPage <= 10)
                return null;

            return BadRequest(new { mensagem = "O parâmetro perPage deve estar entre 1 e 10 (limite do plano free do ApiTube)." });
        }

        private IActionResult CriarRespostaErroImportacao(Exception ex)
        {
            return ex switch
            {
                HttpRequestException => StatusCode(503, new { mensagem = "Não foi possível importar as notícias porque uma API externa (ApiTube ou Gemini) está indisponível.", detalhe = ex.Message }),
                TaskCanceledException => StatusCode(503, new { mensagem = "A requisição a uma API externa excedeu o tempo limite. Tente importar novamente mais tarde." }),
                DbUpdateException => StatusCode(500, new { mensagem = "Erro de banco de dados: Não foi possível salvar as notícias importadas." }),
                _ => StatusCode(500, new { mensagem = "Ocorreu um erro inesperado durante a importação.", detalhe = ex.Message })
            };
        }
    }
}
