using IVnews.Data;
using IVNews.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IVNews.Controllers;

[ApiController]
[Route("api/gerar-imagens")]
public class GeracaoDeImagensController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IGeracaoDeImagensService _geracaoDeImagensService;
    private readonly IArmazenamentoImagemService _armazenamentoImagemService;
    private readonly INoticiaService _noticiaService;

    public GeracaoDeImagensController(AppDbContext context, INoticiaService noticiaService)
    {
        _context = context;
        _noticiaService = noticiaService;
    }

    [HttpPost]
    public async Task<IActionResult> GerarImagem([FromQuery] int noticiaId)
    {
        var article = await _context.Noticias.FirstOrDefaultAsync(n => n.Id == noticiaId);
        var prompt = await _context.Resumos
            .Where(r => r.NoticiaId == noticiaId)
            .Select(r => r.Prompt)
            .FirstOrDefaultAsync();

        if (article == null)
        {
            return NotFound(new { mensagem = "Notícia não encontrada" });
        }
        if (string.IsNullOrWhiteSpace(prompt))
            return BadRequest(new
            {
                mensagem = "Esta notícia não está disponível para geração."
            });
        if (string.IsNullOrWhiteSpace(prompt))
        {
            return BadRequest(new { mensagem = "Geração de imagem indisponível para a notícia especificada" });
        }

        await _noticiaService.GerarESalvarImagemDaNoticiaAsync(article, prompt);

        return Ok(new { mensagem = "Imagem gerada e salva com sucesso", imagemUrl = article.ImagemUrl });
    }
}