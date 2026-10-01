using IVnews.Data;
using IVnews.Model;
using IVNews.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IVNews.Controllers
{
    [ApiController]
    [Route("api/noticias")]
    public class NoticiaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly INoticiaService _noticiaService;

        public NoticiaController(
            AppDbContext context,
            INoticiaService noticiaService)
        {
            _context = context;
            _noticiaService = noticiaService;
        }

        // =====================================================
        // TESTE DA APITUBE
        // GET: api/noticias/apitube?perPage=5
        // =====================================================

        [HttpGet("apitube")]
        public async Task<IActionResult> TestarApiTube([FromQuery] int perPage = 5)
        {
            if (perPage < 1 || perPage > 10)
            {
                return BadRequest(new { mensagem = "O parâmetro perPage deve estar entre 1 e 10 (limite do plano free do ApiTube)." });
            }

            var noticias = await _noticiaService.ObterNoticiasDaApiAsync(perPage);

            return Ok(noticias);
        }

        // POST: api/noticias/apitube/importar?perPage=5
        [HttpPost("apitube/importar")]
       [HttpPost("apitube/importar")]
public async Task<IActionResult> ImportarNoticiasDaApiTube([FromQuery] int perPage = 5)
{
    if (perPage < 1 || perPage > 10)
    {
        return BadRequest(new { mensagem = "O parâmetro perPage deve estar entre 1 e 10 (limite do plano free do ApiTube)." });
    }

    try
    {
        var resultado = await _noticiaService.SalvarNoticiasDaApiAsync(perPage);
        return Ok(resultado);
    }
    catch (HttpRequestException ex)
    {
        return StatusCode(503, new { mensagem = "Não foi possível importar as notícias. O ApiTube está indisponível no momento.", detalhe = ex.Message });
    }
    catch (DbUpdateException)
    {
        return StatusCode(500, new { mensagem = "Erro de banco de dados: Não foi possível salvar as notícias importadas." });
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { mensagem = "Ocorreu um erro inesperado durante a importação.", detalhe = ex.Message });
    }
}
        // =====================================================
        // CRUD DE NOTÍCIAS (Mantido igual)
        // =====================================================
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Noticia>>> GetNoticias()
        {
            var noticias = await _context.Noticias
                .Include(n => n.Categoria)
                .Include(n => n.Localizacao)
                .ToListAsync();

            return Ok(noticias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Noticia>> GetNoticia(int id)
        {
            var noticia = await _context.Noticias
                .Include(n => n.Categoria)
                .Include(n => n.Localizacao)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (noticia == null)
            {
                return NotFound();
            }

            return Ok(noticia);
        }

        [HttpPost]
        public async Task<ActionResult<Noticia>> PostNoticia(Noticia noticia)
        {
            _context.Noticias.Add(noticia);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new { mensagem = "Não foi possível salvar a notícia." });
            }

            return CreatedAtAction(
                nameof(GetNoticia),
                new { id = noticia.Id },
                noticia
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutNoticia(int id, Noticia noticia)
        {
            if (id != noticia.Id)
            {
                return BadRequest();
            }

            _context.Entry(noticia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoticiaExists(id))
                {
                    return NotFound();
                }
                return StatusCode(500, new { mensagem = "Não foi possível atualizar a notícia." });
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new { mensagem = "Não foi possível atualizar a notícia." });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNoticia(int id)
        {
            var noticia = await _context.Noticias.FindAsync(id);

            if (noticia == null)
            {
                return NotFound();
            }

            _context.Noticias.Remove(noticia);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new { mensagem = "Não foi possível excluir a notícia." });
            }

            return NoContent();
        }

        private bool NoticiaExists(int id)
        {
            return _context.Noticias.Any(e => e.Id == id);
        }

        

        
    }
}