using System.Text.Json.Serialization;

namespace IVnews.Model
{
    public class Localizacao
    {
        public int Id { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public string? Pais { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? Tipo { get; set; }
        
        [JsonIgnore]
        public ICollection<Noticia> Noticias { get; set; }
            = new List<Noticia>();
    }
}