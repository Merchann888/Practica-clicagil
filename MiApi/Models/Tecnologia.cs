using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MiApi.Models
{
    public class Tecnologia
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [JsonIgnore]
        public List<Proyecto> Proyectos { get; set; } = new();
    }
}