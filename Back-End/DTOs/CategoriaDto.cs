namespace IVnews.DTOs
{
    public class CategoriaDto
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }

        // Código da categoria na APITube (ex.: medtop:04000000). Opcional.
        public string? IdExterno { get; set; }
    }
}