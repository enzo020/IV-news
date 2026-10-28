using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;
using IVnews.Data;
using IVnews.DTOs;
using IVnews.DTOs.ApiTube;
using IVnews.Model;
using IVNews.Backend.Services;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;

namespace IVNews.Services
{
    public class NoticiaService : INoticiaService
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ImagemService _imagemService;
        private readonly IContentExtractorService _contentExtractor;
        private readonly IGeminiService _geminiService;

        public NoticiaService(HttpClient httpClient, IConfiguration configuration, AppDbContext context, ImagemService imagemService, IContentExtractorService contentExtractor, IGeminiService geminiService)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _context = context;
            _imagemService = imagemService;
            _contentExtractor = contentExtractor;
            _geminiService = geminiService;

            var baseUrl = _configuration["ApiTubeSettings:BaseUrl"];
            var token = _configuration["ApiTubeSettings:Token"];

            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new InvalidOperationException(
                    "ApiTubeSettings:BaseUrl não foi configurado."
                );
            }

            if (string.IsNullOrWhiteSpace(token))
            {
                throw new InvalidOperationException(
                    "ApiTubeSettings:Token não foi configurado."
                );
            }

            _httpClient.BaseAddress = new Uri(baseUrl);

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        // Mapeamento das categorias ApiTube > IvNews
        private static readonly Dictionary<string, int> CategoriaMapeamento = new()
        {
            // Política
            ["medtop:16000000"] = 1,
            ["medtop:20000368"] = 1,
            ["medtop:20000593"] = 1,
            ["medtop:20000577"] = 1,
            ["medtop:20000586"] = 1,
            ["medtop:20000582"] = 1,
            ["medtop:11000000"] = 1,
            ["medtop:20000574"] = 1,

            // Esporte
            ["medtop:15000000"] = 2,
            ["medtop:20001108"] = 2,

            // Tecnologia
            ["medtop:13000000"] = 3,
            ["medtop:20000086"] = 3,
            ["medtop:20000265"] = 3,
            ["medtop:20000731"] = 3,
            ["medtop:20000256"] = 3,
            ["medtop:20000717"] = 3,
            ["medtop:20000257"] = 3,
            ["medtop:20000231"] = 3,
            ["medtop:20000225"] = 3,

            // Saúde
            ["medtop:07000000"] = 4,
            ["medtop:20000461"] = 4,
            ["medtop:20001358"] = 4,
            ["medtop:20000462"] = 4,

            // Economia
            ["medtop:04000000"] = 5,
            ["medtop:20000170"] = 5,
            ["medtop:20000209"] = 5,
            ["medtop:20000200"] = 5,
            ["medtop:20001366"] = 5,
            ["medtop:20000346"] = 5,

            // Negócios
            ["medtop:20000243"] = 7,
            ["medtop:20000209"] = 7,
            ["medtop:20001136"] = 7,
            ["medtop:20001371"] = 7,
            ["medtop:09000000"] = 7,
            ["medtop:20000509"] = 7,
            ["medtop:20000513"] = 7,

            // Educação
            ["medtop:05000000"] = 8,

            // Cultura
            ["medtop:20000038"] = 9,
            ["medtop:20000043"] = 9,
            ["medtop:14000000"] = 9,
            ["medtop:01000000"] = 9,

            // Segurança
            ["medtop:02000000"] = 10,
            ["medtop:20000133"] = 10
        };

        public async Task<List<ApiTubeArticle>> ObterNoticiasDaApiAsync(int perPage)
        {
            var endpoint = $"v1/news/everything?language.code=pt&source.country.code=br&published_at.start=NOW-30DAYS&published_at.end=NOW&per_page={perPage}";

            HttpResponseMessage response;

            try
            {
                // Tenta realizar a requisição à API externa
                response = await _httpClient.GetAsync(endpoint);
            }
            catch (TaskCanceledException)
            {
                throw new HttpRequestException(
                    "[ApiTube Error] A requisição excedeu o tempo limite (Timeout). O serviço do ApiTube pode estar indisponível ou instável."
                );
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(
                    $"[ApiTube Error] Falha de conexão de rede ao tentar acessar o ApiTube. Verifique sua conexão com a internet. Detalhes: {ex.Message}",
                    ex
                );
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Se a API retornar erro HTTP (ex: 502, 503, 504, 500)
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"[ApiTube Error] O ApiTube retornou um erro. Status Code: {(int)response.StatusCode} ({response.StatusCode}). " +
                    $"Detalhes: {jsonResponse}"
                );
            }

            if (string.IsNullOrWhiteSpace(jsonResponse))
            {
                throw new InvalidOperationException(
                    "[ApiTube Error] A API retornou uma resposta com corpo vazio."
                );
            }

            var data = JsonSerializer.Deserialize<ApiTubeResponse>(
                jsonResponse,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );

            if (data?.Results == null)
            {
                throw new InvalidOperationException(
                    $"[ApiTube Error] Falha ao processar os dados recebidos. JSON: {jsonResponse}"
                );
            }

            return data.Results;
        }

        private static string? ObterIdExternoCategoria(ApiTubeCategory categoria)
        {
            return categoria.Links?.Self?
                .Split('/', StringSplitOptions.RemoveEmptyEntries)
                .LastOrDefault();
        }

        private static Categoria? ObterCategoria(ApiTubeArticle article, List<Categoria> categoriasBanco)
        {
            const int CategoriaOutrosId = 6;
            var categorias = article.Categories?
                .OrderByDescending(c => c.Score)
                ?? Enumerable.Empty<ApiTubeCategory>();

            foreach (var categoriaApi in categorias)
            {
                var idExterno = ObterIdExternoCategoria(categoriaApi);

                if (idExterno != null && CategoriaMapeamento.TryGetValue(idExterno, out var categoriaId))
                    return categoriasBanco.FirstOrDefault(c => c.Id == categoriaId);


            }
            return categoriasBanco.FirstOrDefault(c => c.Id == CategoriaOutrosId);
        }

        private async Task<string?> ObterImagemValidaAsync(ApiTubeArticle article)
        {
            if (article.Media == null)
                return null;

            foreach (var media in article.Media)
            {
                if (string.IsNullOrWhiteSpace(media.Url))
                    continue;

                if (await _imagemService.ImagemEhValida(media.Url))
                    return media.Url;
            }

            return null;
        }

        private static double? ConverterCoordenada(string? valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return null;

            return double.TryParse(
                valor,
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out var coordenada)
                    ? coordenada
                    : null;
        }
        private async Task<Localizacao?> ObterOuCriarLocalizacaoAsync(ApiTubeLocation? dados)
        {
            if (dados == null)
                return null;

            var pais = Normalizar(dados.Country);
            var estado = Normalizar(dados.Region);
            var cidade = Normalizar(dados.City);

            // Não cadastrar uma localização completamente vazia.
            if (pais == null && estado == null && cidade == null)
                return null;

            var existente = await _context.Localizacoes.FirstOrDefaultAsync(l =>
                l.Pais == pais &&
                l.Estado == estado &&
                l.Cidade == cidade);

            if (existente != null)
                return existente;

            var localizacao = new Localizacao
            {
                Pais = pais,
                Estado = estado,
                Cidade = cidade,
                Latitude = ConverterCoordenada(dados.Lat),
                Longitude = ConverterCoordenada(dados.Long)
            };

            _context.Localizacoes.Add(localizacao);

            // Persiste para gerar o Id.
            await _context.SaveChangesAsync();

            return localizacao;
        }

        private static string? Normalizar(string? valor)
        {
            return string.IsNullOrWhiteSpace(valor)
                ? null
                : valor.Trim();
        }

        public async Task<Localizacao> SalvarLocalizacaoAsync(LocalizacaoDto localizacaoDto)
        {
            ArgumentNullException.ThrowIfNull(localizacaoDto);

            var pais = Normalizar(localizacaoDto.Pais);
            var estado = Normalizar(localizacaoDto.Estado);
            var cidade = Normalizar(localizacaoDto.Cidade);

            if (pais == null && estado == null && cidade == null)
            {
                throw new ArgumentException(
                    "Informe pelo menos cidade, estado ou país.",
                    nameof(localizacaoDto));
            }

            if (localizacaoDto.Latitude.HasValue &&
                (!double.IsFinite(localizacaoDto.Latitude.Value) ||
                 localizacaoDto.Latitude is < -90 or > 90))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(localizacaoDto),
                    "A latitude deve estar entre -90 e 90.");
            }

            if (localizacaoDto.Longitude.HasValue &&
                (!double.IsFinite(localizacaoDto.Longitude.Value) ||
                 localizacaoDto.Longitude is < -180 or > 180))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(localizacaoDto),
                    "A longitude deve estar entre -180 e 180.");
            }

            var existente = await _context.Localizacoes.FirstOrDefaultAsync(l =>
                l.Pais == pais &&
                l.Estado == estado &&
                l.Cidade == cidade);

            if (existente != null)
                return existente;

            var localizacao = new Localizacao
            {
                Pais = pais,
                Estado = estado,
                Cidade = cidade,
                Latitude = localizacaoDto.Latitude,
                Longitude = localizacaoDto.Longitude
            };

            _context.Localizacoes.Add(localizacao);
            await _context.SaveChangesAsync();

            return localizacao;
        }

        public async Task<Categoria> SalvarCategoriaAsync(CategoriaDto categoriaDto)
        {
            ArgumentNullException.ThrowIfNull(categoriaDto);

            var nome = Normalizar(categoriaDto.Nome);
            var descricao = Normalizar(categoriaDto.Descricao);
            var idExterno = Normalizar(categoriaDto.IdExterno);

            if (nome == null)
            {
                throw new ArgumentException(
                    "Informe o nome da categoria.",
                    nameof(categoriaDto));
            }

            // 1) Procura pelo código da APITube, quando ele foi informado.
            var existente = idExterno != null
                ? await _context.Categorias.FirstOrDefaultAsync(c => c.IdExterno == idExterno)
                : null;

            // 2) Se não achou, procura pelo nome (sem diferenciar maiúsculas de minúsculas).
            if (existente == null)
            {
                var nomeMinusculo = nome.ToLower();

                existente = await _context.Categorias.FirstOrDefaultAsync(c =>
                    c.Nome != null && c.Nome.ToLower() == nomeMinusculo);
            }

            if (existente != null)
            {
                // Completa o que estiver vazio, sem sobrescrever o que já existe.
                if (existente.IdExterno == null && idExterno != null)
                    existente.IdExterno = idExterno;

                if (existente.Descricao == null && descricao != null)
                    existente.Descricao = descricao;

                await _context.SaveChangesAsync();
                return existente;
            }

            var categoria = new Categoria
            {
                Nome = nome,
                Descricao = descricao,
                IdExterno = idExterno
            };

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return categoria;
        }

        // Requisitos da documentação do banco que já dá para conferir ANTES de extrair e chamar o Gemini.
        private static List<string> ValidarArtigoDaApi(ApiTubeArticle article)
        {
            var motivos = new List<string>();

            if (article.Id == 0)
                motivos.Add("Sem id da APITube");

            if (string.IsNullOrWhiteSpace(article.Title))
                motivos.Add("Título vazio");

            if (string.IsNullOrWhiteSpace(article.Source?.Domain))
                motivos.Add("Fonte vazia");

            if (article.PublishedAt == default)
                motivos.Add("Data de publicação ausente");

            return motivos;
        }

        // Confere as regras que o próprio model declara ([Required] na Noticia e
        // [RegularExpression] do Sentimento no Resumo), que são as regras da documentação do banco.
        private static List<string> ValidarRequisitos(object entidade)
        {
            var resultados = new List<ValidationResult>();

            Validator.TryValidateObject(
                entidade,
                new ValidationContext(entidade),
                resultados,
                validateAllProperties: true);

            return resultados
                .Select(r => $"Campo {r.MemberNames.FirstOrDefault() ?? "desconhecido"} ausente ou inválido")
                .ToList();
        }

        // O Gemini pode devolver "positivo" ou "NEUTRO": acerta a escrita antes de validar.
        // Qualquer outro valor é mantido como veio, e a validação rejeita.
        private static string? NormalizarSentimento(string? valor)
        {
            var texto = Normalizar(valor);

            if (texto == null)
                return null;

            return texto.ToLowerInvariant() switch
            {
                "positivo" => "Positivo",
                "negativo" => "Negativo",
                "neutro" => "Neutro",
                _ => texto
            };
        }

        public async Task<ImportacaoResultadoDto> SalvarNoticiasDaApiAsync(int perPage)
        {
            var noticiasApi = await ObterNoticiasDaApiAsync(perPage);

            int inseridas = 0;
            int ignoradas = 0;
            int rejeitadas = 0;
            int falhasExtracao = 0;
            int falhasProcessamento = 0;
            var motivosRejeicao = new List<string>();

            var categoriasBanco = await _context.Categorias.ToListAsync();

            // Registra a notícia rejeitada e o motivo, que aparece no resultado da importação.
            void Rejeitar(ApiTubeArticle artigo, IEnumerable<string> motivos)
            {
                rejeitadas++;
                motivosRejeicao.Add($"[{artigo.Id}] {artigo.Title}: {string.Join("; ", motivos)}");
            }

            foreach (var article in noticiasApi)
            {
                // 0. Rejeita o que não cumpre os requisitos do banco, antes de gastar extração e Gemini.
                var motivosApi = ValidarArtigoDaApi(article);

                if (motivosApi.Count > 0)
                {
                    Rejeitar(article, motivosApi);
                    continue;
                }

                var noticiaExistente = await _context.Noticias
                    .FirstOrDefaultAsync(
                        n => n.IdExterno == article.Id.ToString());

                if (noticiaExistente != null)
                {
                    ignoradas++;
                    continue;
                }

                // 1. Extrai o conteúdo da página original.
                var extraction = await _contentExtractor.ExtractAsync(
                    article.Href ?? "");

                if (string.IsNullOrWhiteSpace(extraction.Text))
                {
                    falhasExtracao++;
                    continue;
                }

                // 2. Processa o texto extraído com o Gemini.
                ProcessamentoNoticiaDto? processada;

                try
                {
                    processada = await _geminiService.ProcessarNoticiaAsync(extraction.Text);
                }
                catch (Exception ex) when (
                    ex is JsonException ||
                    ex is InvalidOperationException)
                {
                    falhasProcessamento++;
                    continue;
                }

                if (processada == null ||
                    string.IsNullOrWhiteSpace(processada.ConteudoTratado) ||
                    string.IsNullOrWhiteSpace(processada.ResumoExecutivo) ||
                    string.IsNullOrWhiteSpace(processada.Descricao))
                {
                    falhasProcessamento++;
                    continue;
                }

                // 3. Monta a notícia (texto tratado, não o Body do ApiTube) e o resumo associado.
                var noticia = new Noticia
                {
                    Titulo = article.Title,
                    Descricao = processada.Descricao,

                    Conteudo = processada.ConteudoTratado,

                    Autor = article.Author?.Name,
                    Fonte = article.Source?.Domain,
                    UrlNoticia = article.Href,
                    IdExterno = article.Id.ToString(),
                    PublicadoEm = article.PublishedAt
                };

                var resumo = new Resumo
                {
                    Titulo = noticia.Titulo,
                    ResumoExecutivo = processada.ResumoExecutivo,
                    PontosChave = processada.PontosChave ?? new(),
                    Sentimento = NormalizarSentimento(processada.Sentimento),
                    Prompt = processada.Prompt,

                    Noticia = noticia
                };

                // 4. Confere as regras da documentação do banco. Se falhar, rejeita e segue para a próxima.
                var motivosModelo = ValidarRequisitos(noticia)
                    .Concat(ValidarRequisitos(resumo))
                    .ToList();

                if (motivosModelo.Count > 0)
                {
                    Rejeitar(article, motivosModelo);
                    continue;
                }

                // 5. Recupera os metadados (só para quem passou na validação).
                noticia.Categoria = ObterCategoria(article, categoriasBanco);
                noticia.ImagemUrl = await ObterImagemValidaAsync(article);
                noticia.Localizacao = await ObterOuCriarLocalizacaoAsync(article.Location);

                _context.Noticias.Add(noticia);
                _context.Resumos.Add(resumo);

                inseridas++;
            }

            await _context.SaveChangesAsync();

            return new ImportacaoResultadoDto
            {
                TotalRecebidoDaApi = noticiasApi.Count,
                TotalInserido = inseridas,
                TotalIgnoradasDuplicadas = ignoradas,
                TotalRejeitadas = rejeitadas,
                MotivosRejeicao = motivosRejeicao,

                Mensagem =
                    $"Importação finalizada. " +
                    $"Recebidas: {noticiasApi.Count} | " +
                    $"Inseridas: {inseridas} | " +
                    $"Duplicadas: {ignoradas} | " +
                    $"Rejeitadas: {rejeitadas} | " +
                    $"Falhas na extração: {falhasExtracao} | " +
                    $"Falhas no Gemini: {falhasProcessamento}"
            };
        }
    }
}