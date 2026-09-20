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
}