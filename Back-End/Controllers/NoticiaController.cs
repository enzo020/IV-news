using System.Text.Json;
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
        private readonly IGeminiService _geminiService;

        public NoticiaController(
            AppDbContext context,
            INoticiaService noticiaService,
            IGeminiService geminiService)
        {
            _context = context;
            _noticiaService = noticiaService;
            _geminiService = geminiService;
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

        [HttpPost("{id}/resumo")]
        public async Task<IActionResult> GerarResumoDaNoticia(int id)
        {
            var noticia = await _context.Noticias
                .FirstOrDefaultAsync(n => n.Id == id);

            if (noticia == null)
            {
                return NotFound(new { mensagem = $"Notícia com ID {id} não encontrada." });
            }

            if (string.IsNullOrWhiteSpace(noticia.Conteudo))
            {
                return BadRequest(new { mensagem = "A notícia não possui conteúdo para resumo." });
            }

            try
            {
                var resumoJson = await _geminiService.GerarResumoAsync(noticia.Conteudo);

                if (string.IsNullOrWhiteSpace(resumoJson))
                {
                    return StatusCode(502, new { mensagem = "O Gemini não retornou um resumo válido." });
                }

                using var document = JsonDocument.Parse(resumoJson);
                var root = document.RootElement;

                var resumo = new Resumo
                {
                    Texto = root.TryGetProperty("resumo_executivo", out var resumoExecutivo)
                        ? resumoExecutivo.GetString()
                        : noticia.Conteudo,
                    Titulo = root.TryGetProperty("titulo", out var titulo) ? titulo.GetString() : noticia.Titulo,
                    ResumoExecutivo = root.TryGetProperty("resumo_executivo", out var resumoExecutivoTexto)
                        ? resumoExecutivoTexto.GetString()
                        : noticia.Conteudo,
                    PontosChave = root.TryGetProperty("pontos_chave", out var pontosChave)
                        ? pontosChave.EnumerateArray()
                            .Select(item => item.GetString())
                            .Where(item => !string.IsNullOrWhiteSpace(item))
                            .ToList() ?? new List<string>()
                        : new List<string>(),
                    Categoria = root.TryGetProperty("categoria", out var categoria) ? categoria.GetString() : "Outros",
                    Sentimento = root.TryGetProperty("sentimento", out var sentimento) ? sentimento.GetString() : "Neutro",
                    NoticiaId = noticia.Id,
                    Noticia = noticia
                };

                _context.Resumos.Add(resumo);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    noticiaId = noticia.Id,
                    titulo = resumo.Titulo,
                    resumo_executivo = resumo.ResumoExecutivo,
                    pontos_chave = resumo.PontosChave,
                    categoria = resumo.Categoria,
                    sentimento = resumo.Sentimento
                });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(502, new { mensagem = "Não foi possível gerar o resumo com o Gemini.", detalhe = ex.Message });
            }
            catch (JsonException)
            {
                return StatusCode(502, new { mensagem = "O Gemini retornou um JSON inválido para o resumo da notícia." });
            }
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