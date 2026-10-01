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
        public ApiTubeSummary Summary { get; set; }
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
        public string Slug { get; set; } // Opcional: útil para identificação amigável
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
    }
}