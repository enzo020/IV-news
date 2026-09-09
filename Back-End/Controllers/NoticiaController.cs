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
    }
}