using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MiApi.Dtos
{
    public class ProyectoRequestDto
    {
        [Required]
        [MaxLength(150)]
        [JsonPropertyName("name")]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(500)]
        [JsonPropertyName("description")]
        public string Descripcion { get; set; } = string.Empty;

        [JsonPropertyName("technologyIds")]
        public List<int> TecnologiaIds { get; set; } = new();
    }
}