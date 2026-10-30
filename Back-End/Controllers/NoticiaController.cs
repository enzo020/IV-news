using IVnews.Data;
using IVnews.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IVNews.Controllers
{
    [ApiController]
    [Route("api/noticias")]
    public class NoticiaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NoticiaController(AppDbContext context)
        {
            _context = context;
        }

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

        [HttpGet("{id}/resumo")]
        public async Task<IActionResult> ObterResumoDaNoticia(int id)
        {
            var resumo = await _context.Resumos
                .FirstOrDefaultAsync(r => r.NoticiaId == id);

            if (resumo == null)
            {
                return NotFound(new { mensagem = $"Resumo para a notícia com ID {id} não encontrado." });
            }

            return Ok(resumo);
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