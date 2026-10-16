using System.Text;
using System.Text.Json;

public class GeminiService : IGeminiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public GeminiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;

        _httpClient.BaseAddress = new Uri("https://generativelanguage.googleapis.com/");
    }

    public async Task<string?> GerarResumoAsync(string conteudo)
    {
        if (string.IsNullOrWhiteSpace(conteudo))
        {
            throw new ArgumentException("O conteúdo da notícia não pode estar vazio.", nameof(conteudo));
        }

        var apiKey = _configuration["Gemini:ApiKey"];
        var model = _configuration["Gemini:Model"] ?? "gemini-2.5-flash";

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new InvalidOperationException("Gemini:ApiKey não foi configurado.");
        }

        var prompt = new StringBuilder();
        prompt.AppendLine("Role: Você é um assistente especialista em síntese de notícias para feeds rápidos.");
        prompt.AppendLine("Instrução: Analise o texto da notícia fornecido e crie um resumo objetivo. Retorne estritamente um objeto JSON sem blocos de código markdown ou texto explicativo extra.");
        prompt.AppendLine("Regras:");
        prompt.AppendLine("- Mantenha o tom neutro e baseado estritamente no texto fornecido.");
        prompt.AppendLine("- O campo resumo_executivo não deve ultrapassar 60 palavras.");
        prompt.AppendLine("- O campo pontos_chave deve conter exatamente 3 itens curtos.");
        prompt.AppendLine("- Os valores válidos para categoria devem seguir a lista: Tecnologia, Economia, Política, Esporte, Saúde, Outros.");
        prompt.AppendLine("- Os valores válidos para sentimento devem ser: Positivo, Negativo ou Neutro.");
        prompt.AppendLine("- A resposta deve ser um JSON válido com os campos: titulo, resumo_executivo, pontos_chave, categoria, sentimento.");
        prompt.AppendLine();
        prompt.AppendLine("Texto da Notícia:");
        prompt.Append(conteudo);

        var payload = new
        {
            contents = new[]
            {
                new
                {
                    role = "user",
                    parts = new[]
                    {
                        new { text = prompt.ToString() }
                    }
                }
            },
            generationConfig = new
            {
                temperature = 0.2,
                response_mime_type = "application/json"
            }
        };

        using var response = await _httpClient.PostAsJsonAsync(
            $"v1beta/models/{model}:generateContent?key={apiKey}",
            payload
        );

        var responseBody = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Erro ao consultar o Gemini. Status: {(int)response.StatusCode} ({response.StatusCode}). Detalhe: {responseBody}"
            );
        }

        if (string.IsNullOrWhiteSpace(responseBody))
        {
            return null;
        }

        using var document = JsonDocument.Parse(responseBody);

        var text = document.RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString();

        return string.IsNullOrWhiteSpace(text) ? null : text;
    }
}
