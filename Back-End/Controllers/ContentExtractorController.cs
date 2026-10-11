
using IVNews.Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IVNews.Backend;
using IVnews.Data;

namespace IVNews.Backend.Controllers;

[ApiController]
[Route("api/extrator")]
public class ContentExtractorController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IContentExtractorService _extractor;

    public ContentExtractorController(AppDbContext context, IContentExtractorService extractor)
    {
        _context = context;
        _extractor = extractor;
    }

    [HttpGet]
    public async Task<IActionResult> Testar(
        [FromQuery] int quantidade = 5)
    {
        quantidade = Math.Clamp(quantidade, 1, 10);

        var noticias = await _context.Noticias
            .Where(n => n.UrlNoticia != null &&
                        n.UrlNoticia != "")
            .Take(quantidade)
            .ToListAsync();

        var resultados = new List<object>();

        foreach (var noticia in noticias)
        {
            var resultado = await _extractor.ExtractAsync(noticia.UrlNoticia!);

            resultados.Add(new
            {
                noticia.Id,
                noticia.Titulo,
                noticia.UrlNoticia,
                resultado.IsReadable,
                resultado.Completed,
                TamanhoTexto = resultado.Text.Length,
                Amostra = resultado.Text,
                resultado.Error
            });
        }

        return Ok(resultados);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ExtrairPorId(int id)
    {
        var noticia = await _context.Noticias.FirstOrDefaultAsync(n => n.Id == id);

        if (noticia == null)
        {
            return NotFound(new
            {
                mensagem = $"Notícia com ID {id} não encontrada."
            });
        }

        if (string.IsNullOrWhiteSpace(noticia.UrlNoticia))
        {
            return BadRequest(new
            {
                mensagem = "A notícia não possui uma URL válida."
            });
        }

        var resultado = await _extractor.ExtractAsync(noticia.UrlNoticia);

        return Ok(new
        {
            noticia.Id,
            noticia.Titulo,
            noticia.UrlNoticia,
            resultado.IsReadable,
            resultado.Completed,
            TamanhoTexto = resultado.Text.Length,
            Texto = resultado.Text,
            resultado.Error
        });
    }
}
