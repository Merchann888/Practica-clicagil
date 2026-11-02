using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MiApi.Dtos
{
    public class TecnologiaRequestDto
    {
        [Required]
        [MaxLength(100)]
        [JsonPropertyName("name")]
        public string Nombre { get; set; } = string.Empty;
    }
}