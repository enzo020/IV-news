
using IVNews.Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IVNews.Backend;
using IVnews.Data;

namespace IVNews.Backend.Controllers;

[ApiController]
[Route("api/teste-extrator")]
public class ContentExtractorTestController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ContentExtractorService _extractor;

    public ContentExtractorTestController(
        AppDbContext context,
        ContentExtractorService extractor)
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
            var resultado = await _extractor.ExtractAsync(
                noticia.UrlNoticia!);

            resultados.Add(new
            {
                noticia.Id,
                noticia.Titulo,
                noticia.UrlNoticia,
                resultado.IsReadable,
                resultado.Completed,
                TamanhoTexto = resultado.Text.Length,
                Amostra = resultado.Text.Length > 1000
                    ? resultado.Text
                    : resultado.Text,
                resultado.Error
            });
        }

        return Ok(resultados);
    }
}
