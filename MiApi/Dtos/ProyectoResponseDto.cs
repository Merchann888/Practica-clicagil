using System.Text.Json.Serialization;

namespace MiApi.Dtos
{
    public class ProyectoResponseDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Nombre { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Descripcion { get; set; } = string.Empty;

        [JsonPropertyName("createdAt")]
        public DateTime FechaCreacion { get; set; }

        [JsonPropertyName("technologies")]
        public List<TecnologiaResponseDto> Tecnologias { get; set; } = new();
    }
}