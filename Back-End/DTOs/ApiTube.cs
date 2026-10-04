// Este arquivo é responsável por definir os DTOs (Data Transfer Objects) utilizados para representar a resposta da API do ApiTube.

namespace IVnews.DTOs.ApiTube
{
    
    public class ApiTubeResponse
    {
        public string Status { get; set; }
        public List<ApiTubeArticle> Results { get; set; }
    }

    public class ApiTubeArticle
    {
        public long Id { get; set; }
        public string Href { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public ApiTubeAuthor Author { get; set; }
        public ApiTubeSource Source { get; set; }
        public List<ApiTubeMedia> Media { get; set; }
        
        // Novas entidades mapeadas
        public List<ApiTubeCategory> Categories { get; set; }
        // public ApiTubeSummary Summary { get; set; } (temporariamente retirado para testes)
        public ApiTubeLocation Location { get; set; }
    }

    public class ApiTubeAuthor
    {
        public string Name { get; set; }
    }

    public class ApiTubeSource
    {
        public string Domain { get; set; }
    }

    public class ApiTubeMedia
    {
        public string Url { get; set; }
    }

    // --- Novas Classes Mapeadas ---

    public class ApiTubeCategory
    {
        public string Name { get; set; }
        public double Score { get; set; }
        public string Taxonomy { get; set; }
        public ApiTubeLinks Links { get; set; }
    }

    public class ApiTubeLinks
    {
        public string Self { get; set; }
    }

    public class ApiTubeSummary
    {
        public string Text { get; set; }
        public List<string> KeyPoints { get; set; } // Opcional: caso a API traga tópicos principais do resumo
    }

    public class ApiTubeLocation
    {
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
    }
}