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
        public async Task<IActionResult> GetNoticias()
        {
            var noticias = await _context.Noticias
                .Include(n => n.Categoria)
                .Include(n => n.Localizacoes)
                .ToListAsync();

            var noticiasComPrompt = await _context.Resumos
                .Where(r => !string.IsNullOrWhiteSpace(r.Prompt))
                .Select(r => r.NoticiaId)
                .Distinct()
                .ToListAsync();

            var resultado = noticias.Select(noticia => new
            {
                noticia.Id,
                noticia.Titulo,
                noticia.Conteudo,
                noticia.Autor,
                noticia.Fonte,
                noticia.UrlNoticia,
                noticia.ImagemUrl,
                noticia.IdExterno,
                noticia.Descricao,
                noticia.PublicadoEm,
                noticia.Categoria,
                noticia.Localizacoes,

                possuiPrompt = noticiasComPrompt.Contains(noticia.Id)
            });

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNoticia(int id)
        {
            var noticia = await _context.Noticias
                .Include(n => n.Categoria)
                .Include(n => n.Localizacoes)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (noticia == null)
            {
                return NotFound();
            }

            var possuiPrompt = await _context.Resumos
                .AnyAsync(r =>
                    r.NoticiaId == id &&
                    !string.IsNullOrWhiteSpace(r.Prompt)
                );


            return Ok(new
            {
                noticia,
                possuiPrompt
            });
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
            var idsLocalizacoes = noticia.Localizacoes
                .Select(l => l.Id)
                .Distinct()
                .ToList();

            noticia.Localizacoes = await _context.Localizacoes
                .Where(l => idsLocalizacoes.Contains(l.Id))
                .ToListAsync();

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
                return BadRequest();

            var noticiaExistente = await _context.Noticias
                .Include(n => n.Localizacoes)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (noticiaExistente == null)
                return NotFound();

            // Atualiza os campos escalares da notícia.
            _context.Entry(noticiaExistente)
                .CurrentValues.SetValues(noticia);

            // Atualiza as associações de localizações.
            var idsLocalizacoes = noticia.Localizacoes
                .Select(l => l.Id)
                .Distinct()
                .ToList();

            var localizacoes = await _context.Localizacoes
                .Where(l => idsLocalizacoes.Contains(l.Id))
                .ToListAsync();

            noticiaExistente.Localizacoes = localizacoes;

            try
            {
                await _context.SaveChangesAsync();
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