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

        // GET: api/noticias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Noticia>>> GetNoticias()
        {
            var noticias = await _context.Noticias
                .Include(n => n.Categoria)
                .Include(n => n.Localizacao)
                .ToListAsync();
            return Ok(noticias);
        }

        // GET: api/noticias/{id}
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

        // POST: api/noticias
        [HttpPost]
        public async Task<ActionResult<Noticia>> PostNoticia(Noticia noticia)
        {
            _context.Noticias.Add(noticia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNoticia), new { id = noticia.Id }, noticia);
        }

        // PUT: api/noticias/{id}
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
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/noticias/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNoticia(int id)
        {
            var noticia = await _context.Noticias.FindAsync(id);
            if (noticia == null)
            {
                return NotFound();
            }

            _context.Noticias.Remove(noticia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NoticiaExists(int id)
        {
            return _context.Noticias.Any(e => e.Id == id);
        }
    }
}