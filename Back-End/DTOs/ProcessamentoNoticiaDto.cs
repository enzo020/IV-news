using System.Text.Json.Serialization;

public class ProcessamentoNoticiaDto
{
    [JsonPropertyName("conteudo_tratado")]
    public string? ConteudoTratado { get; set; }

    [JsonPropertyName("resumo_executivo")]
    public string? ResumoExecutivo { get; set; }

    [JsonPropertyName("pontos_chave")]
    public List<string> PontosChave { get; set; } = new();

    [JsonPropertyName("sentimento")]
    public string? Sentimento { get; set; }

    [JsonPropertyName("descricao")]
    public string? Descricao { get; set; }
}