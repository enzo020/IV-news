using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using IVnews.Data;
using IVnews.DTOs;
using IVnews.DTOs.ApiTube;
using IVnews.Model;
using IVNews.Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace IVNews.Services
{
    public class NoticiaService : INoticiaService
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ImagemService _imagemService;
        private readonly ArmazenamentoImagemService _armazenamentoImagemService;
        private readonly IContentExtractorService _contentExtractor;
        private readonly IGeminiService _geminiService;
        private readonly IGeracaoDeImagensService _geracaoDeImagensService;

        public NoticiaService(HttpClient httpClient, IConfiguration configuration, AppDbContext context, ImagemService imagemService, ArmazenamentoImagemService armazenamentoImagemService, IContentExtractorService contentExtractor, IGeminiService geminiService, IGeracaoDeImagensService geracaoDeImagensService)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _context = context;
            _imagemService = imagemService;
            _armazenamentoImagemService = armazenamentoImagemService;
            _geracaoDeImagensService = geracaoDeImagensService;
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

        // sobrecar o método para aceitar um parâmetro de categoria do banco, e filtrar as notícias da API com base nesse parâmetro.
        public async Task<List<ApiTubeArticle>> ObterNoticiasDaApiAsync(
            int perPage,
            int categoriaBancoId)
        {
            const int CategoriaOutrosId = 6;

            // Outros não utiliza category.id.
            var gruposCategorias = categoriaBancoId == CategoriaOutrosId
                ? new List<string[]> { Array.Empty<string>() }
                : CategoriaMapeamento
                    .Where(c => c.Value == categoriaBancoId)
                    .Select(c => c.Key)
                    .Distinct()
                    .Chunk(3)
                    .Select(grupo => grupo.ToArray())
                    .ToList();

            if (gruposCategorias.Count == 0)
                throw new ArgumentException(
                    $"Não existem categorias APITube associadas ao ID {categoriaBancoId}.");

            var idsExistentes = (await _context.Noticias
                .Where(n => n.IdExterno != null)
                .Select(n => n.IdExterno!)
                .ToListAsync())
                .ToHashSet();

            var noticias = new List<ApiTubeArticle>();
            var idsSelecionados = new HashSet<string>();

            foreach (var grupo in gruposCategorias)
            {
                var resultados = await BuscarNoticiasPorCategoriaAsync(
                    perPage,
                    grupo);

                foreach (var noticia in resultados)
                {
                    string id = noticia.Id.ToString();

                    if (idsExistentes.Contains(id) ||
                        !idsSelecionados.Add(id))
                        continue;

                    noticias.Add(noticia);
                }
            }

            return noticias;
        }


        private async Task<List<ApiTubeArticle>> BuscarNoticiasPorCategoriaAsync(
            int perPage,
            string[] categoriasApi)
        {
            var endpoint =
                "v1/news/everything" +
                "?language.code=pt" +
                "&source.country.code=br";

            if (categoriasApi.Length > 0)
            {
                var categorias = Uri.EscapeDataString(
                    string.Join(",", categoriasApi));

                endpoint += $"&category.id={categorias}";
            }

            endpoint += $"&per_page={perPage}";

            HttpResponseMessage response;

            try
            {
                response = await _httpClient.GetAsync(endpoint);
            }
            catch (TaskCanceledException)
            {
                throw new HttpRequestException(
                    "[ApiTube Error] A requisição excedeu o tempo limite.");
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(
                    $"[ApiTube Error] Falha de conexão: {ex.Message}", ex);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"[ApiTube Error] Status: {(int)response.StatusCode}. " +
                    $"Detalhes: {jsonResponse}");
            }

            if (string.IsNullOrWhiteSpace(jsonResponse))
                throw new InvalidOperationException(
                    "[ApiTube Error] Resposta vazia.");

            var data = JsonSerializer.Deserialize<ApiTubeResponse>(
                jsonResponse,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (data?.Results == null)
                throw new InvalidOperationException(
                    "[ApiTube Error] Não foi possível processar a resposta.");

            return data.Results;
        }


        public async Task<List<ApiTubeArticle>> ObterNoticiasDaApiAsync(int perPage)
        {
            const int quantidadePeriodos = 5;
            const int intervaloDias = 20;
            const int tamanhoPagina = 10;
            const int limitePaginas = 5;

            var hoje = DateTime.UtcNow.Date;
            var inicio = hoje.AddDays(-100);

            // IDs externos que já estão cadastrados no banco.
            var idsExistentes = (await _context.Noticias
                .Where(n => n.IdExterno != null)
                .Select(n => n.IdExterno!)
                .ToListAsync())
                .ToHashSet();

            var noticias = new List<ApiTubeArticle>();
            var idsSelecionados = new HashSet<string>();

            for (int i = 0; i < quantidadePeriodos; i++)
            {
                if (noticias.Count >= perPage)
                    break;

                var dataInicio = inicio.AddDays(i * intervaloDias);

                var dataFim = i == quantidadePeriodos - 1
                    ? hoje
                    : dataInicio.AddDays(intervaloDias - 1);

                // Distribui a quantidade restante entre os períodos restantes.
                int periodosRestantes = quantidadePeriodos - i;
                int quantidadeAlvo = (int)Math.Ceiling(
                    (double)(perPage - noticias.Count) / periodosRestantes
                );

                int novasNoPeriodo = 0;

                for (int pagina = 1; pagina <= limitePaginas; pagina++)
                {
                    if (novasNoPeriodo >= quantidadeAlvo ||
                        noticias.Count >= perPage)
                        break;

                    var resultados = await BuscarNoticiasPorPeriodoAsync(
                        dataInicio,
                        dataFim,
                        tamanhoPagina,
                        pagina
                    );

                    if (resultados.Count == 0)
                        break;

                    foreach (var noticia in resultados)
                    {
                        string id = noticia.Id.ToString();

                        // Ignora notícias já importadas ou repetidas nesta execução.
                        if (idsExistentes.Contains(id) ||
                            !idsSelecionados.Add(id))
                            continue;

                        noticias.Add(noticia);
                        novasNoPeriodo++;

                        if (novasNoPeriodo >= quantidadeAlvo ||
                            noticias.Count >= perPage)
                            break;
                    }
                }
            }

            return noticias;
        }
        private async Task<List<ApiTubeArticle>> BuscarNoticiasPorPeriodoAsync(
            DateTime inicio,
            DateTime fim,
            int perPage,
            int page)
        {
            var endpoint =
                $"v1/news/everything" +
                $"?language.code=pt" +
                $"&source.country.code=br" +
                $"&published_at.start={inicio:yyyy-MM-dd}" +
                $"&published_at.end={fim:yyyy-MM-dd}" +
                $"&sort.by=published_at" +
                $"&sort.order=desc" +
                $"&per_page={perPage}" +
                $"&page={page}";


            HttpResponseMessage response;

            try
            {
                response = await _httpClient.GetAsync(endpoint);
            }
            catch (TaskCanceledException)
            {
                throw new HttpRequestException(
                    "[ApiTube Error] A requisição excedeu o tempo limite."
                );
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(
                    $"[ApiTube Error] Falha de conexão: {ex.Message}", ex
                );
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"[ApiTube Error] Status: {(int)response.StatusCode}. " +
                    $"Detalhes: {jsonResponse}"
                );
            }

            if (string.IsNullOrWhiteSpace(jsonResponse))
                throw new InvalidOperationException(
                    "[ApiTube Error] Resposta vazia."
                );

            var data = JsonSerializer.Deserialize<ApiTubeResponse>(
                jsonResponse,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );

            if (data?.Results == null)
                throw new InvalidOperationException(
                    "[ApiTube Error] Não foi possível processar a resposta."
                );

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
            if (article.Media == null || !article.Media.Any())
                return null;

            string? melhorImagem = null;
            long maiorPontuacao = 0;

            foreach (var media in article.Media.Take(8))
            {
                if (string.IsNullOrWhiteSpace(media.Url))
                    continue;

                var pontuacao = await _imagemService
                    .ObterPontuacaoImagemAsync(media.Url);

                if (pontuacao == null || pontuacao <= maiorPontuacao)
                    continue;

                maiorPontuacao = pontuacao.Value;
                melhorImagem = media.Url;
            }

            return melhorImagem;
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
        private async Task<List<Localizacao>> ObterOuCriarLocalizacoesAsync(IEnumerable<ApiTubeLocation>? dados)
        {
            var localizacoes = new List<Localizacao>();

            if (dados == null)
                return localizacoes;

            foreach (var dado in dados)
            {
                var cidade = Normalizar(dado.Name);
                var pais = Normalizar(dado.Country);
                var tipo = Normalizar(dado.Type);

                if (cidade == null)
                    continue;

                if (localizacoes.Any(l => l.Cidade == cidade && l.Pais == pais && l.Tipo == tipo))
                    continue;

                var existente = _context.Localizacoes.Local
                    .FirstOrDefault(l =>
                        l.Cidade == cidade &&
                        l.Pais == pais &&
                        l.Tipo == tipo);

                existente ??= await _context.Localizacoes
                    .FirstOrDefaultAsync(l =>
                        l.Cidade == cidade &&
                        l.Pais == pais &&
                        l.Tipo == tipo);

                if (existente != null)
                {
                    localizacoes.Add(existente);
                    continue;
                }
                
                var novaLocalizacao = new Localizacao
                {
                    Cidade = cidade,
                    Pais = pais,
                    Tipo = tipo,
                    Latitude = dado.Lat,
                    Longitude = dado.Lng 
                };

                _context.Localizacoes.Add(novaLocalizacao);
                localizacoes.Add(novaLocalizacao);
            }

            return localizacoes;
        }

        private static string? Normalizar(string? valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return null;

            return valor.Trim().ToLowerInvariant();
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

        // Normaliza o título para evitar duplicatas, removendo acentos, pontuação e espaços duplicados.
        private string NormalizarTitulo(string? titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                return string.Empty;

            string normalizado = titulo.ToLowerInvariant();

            // Remove acentos.
            normalizado = normalizado.Normalize(NormalizationForm.FormD);

            normalizado = new string(
                normalizado
                    .Where(c => CharUnicodeInfo.GetUnicodeCategory(c)
                        != UnicodeCategory.NonSpacingMark)
                    .ToArray()
            );

            normalizado = normalizado.Normalize(NormalizationForm.FormC);

            // Remove pontuação e caracteres especiais.
            normalizado = Regex.Replace(normalizado, @"[^\w\s]", " ");

            // Remove espaços duplicados.
            normalizado = Regex.Replace(normalizado, @"\s+", " ").Trim();

            return normalizado;
        }

        // Calcula a similaridade usando fórmula de Jaccard entre dois títulos, para evitar duplicatas.
        private double CalcularSimilaridadeJaccard(string tituloA, string tituloB)
        {
            var palavrasA = tituloA
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .ToHashSet();

            var palavrasB = tituloB
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .ToHashSet();

            if (palavrasA.Count == 0 || palavrasB.Count == 0)
                return 0;

            int intersecao = palavrasA.Intersect(palavrasB).Count();
            int uniao = palavrasA.Union(palavrasB).Count();

            return (double)intersecao / uniao;
        }

        private bool EhTituloDuplicado(
            string? titulo,
            IEnumerable<string?> titulosExistentes,
            double limite = 0.80)
        {
            var tituloNormalizado = NormalizarTitulo(titulo);

            if (string.IsNullOrWhiteSpace(tituloNormalizado))
                return false;

            foreach (var tituloExistente in titulosExistentes)
            {
                var existenteNormalizado = NormalizarTitulo(tituloExistente);

                if (string.IsNullOrWhiteSpace(existenteNormalizado))
                    continue;

                double similaridade = CalcularSimilaridadeJaccard(
                    tituloNormalizado,
                    existenteNormalizado
                );

                if (similaridade >= limite)
                    return true;
            }

            return false;
        }

        public async Task GerarESalvarImagemDaNoticiaAsync(Noticia noticia, string? prompt)
        {
            if (noticia == null)
                throw new ArgumentNullException(nameof(noticia));

            if (prompt == null)
                throw new ArgumentNullException(nameof(prompt));

            var imagemBytes = await _geracaoDeImagensService.GerarImagemAsync(prompt);
            var publicId = $"noticias/{Guid.NewGuid()}";

            noticia.ImagemUrl = await _armazenamentoImagemService.SalvarImagemAsync(
                imagemBytes,
                publicId
            );
            await _context.SaveChangesAsync();
        }

        public Task<ImportacaoResultadoDto> SalvarNoticiasDaApiAsync(
            int perPage)    
        {
            return SalvarNoticiasDaApiInternoAsync(
                perPage,
                categoriaBancoId: null);
        }

        public Task<ImportacaoResultadoDto> SalvarNoticiasDaApiAsync(
            int perPage,
            int categoriaBancoId)
        {
            return SalvarNoticiasDaApiInternoAsync(
                perPage,
                categoriaBancoId);
        }

        public async Task<ImportacaoResultadoDto> SalvarNoticiasDaApiInternoAsync(int perPage, int? categoriaBancoId)
        {
            Console.WriteLine("Iniciando importação de notícias da API...");
            var noticiasApi = categoriaBancoId.HasValue
                ? await ObterNoticiasDaApiAsync(perPage, categoriaBancoId.Value)
                : await ObterNoticiasDaApiAsync(perPage);


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

            var titulosExistentes = await _context.Noticias
                .Where(n => n.PublicadoEm >= DateTime.UtcNow.AddDays(-100))
                .Select(n => n.Titulo)
                .ToListAsync();

            var titulosImportados = new List<string>();

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

                var titulosParaComparar = titulosExistentes.Concat(titulosImportados);
                
                if (EhTituloDuplicado(article.Title, titulosParaComparar))
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

                // 5. Recupera os metadados.
                noticia.Categoria = ObterCategoria(article, categoriasBanco);
                noticia.ImagemUrl = await ObterImagemValidaAsync(article);

                noticia.Localizacoes = await ObterOuCriarLocalizacoesAsync(article.LocationsMentioned);

                _context.Noticias.Add(noticia);
                _context.Resumos.Add(resumo);

                titulosImportados.Add(noticia.Titulo!);

                inseridas++;
                await _context.SaveChangesAsync();
                await Task.Delay(TimeSpan.FromSeconds(5));
            }


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