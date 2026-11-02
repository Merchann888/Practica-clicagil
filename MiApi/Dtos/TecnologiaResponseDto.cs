using System.Text.Json.Serialization;

namespace MiApi.Dtos
{
    public class TecnologiaResponseDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Nombre { get; set; } = string.Empty;
    }
}