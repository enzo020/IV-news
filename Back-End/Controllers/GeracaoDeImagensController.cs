using IVnews.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IVNews.Controllers;

[ApiController]
[Route("api/gerar-imagens")]
public class GeracaoDeImagensController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IGeracaoDeImagensService _geracaoDeImagensService;

    public GeracaoDeImagensController(AppDbContext context, IGeracaoDeImagensService geracaoDeImagensService)
    {
        _context = context;
        _geracaoDeImagensService = geracaoDeImagensService;
    }

    [HttpPost]
    public async Task<IActionResult> GerarImagem(int noticiaId)
    {
        var prompt = await _context.Resumos
            .Where(r => r.NoticiaId == noticiaId)
            .Select(r => r.Prompt)
            .FirstOrDefaultAsync();

        if (prompt == null)
        {
            return NotFound(new { mensagem = "Notícia não encontrada" });
        }
        if (string.IsNullOrWhiteSpace(prompt))
        {
            return BadRequest(new { mensagem = "Geração de imagem indisponível para a notícia especificada" });
        }

        var imagem64 = await _geracaoDeImagensService.GerarImagemAsync(prompt);
        return Ok(new { imagem64 });
    }
}