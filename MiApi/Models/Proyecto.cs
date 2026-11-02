using System.ComponentModel.DataAnnotations;

namespace MiApi.Models
{
    public class Proyecto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Descripcion { get; set; } = string.Empty;

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        // Relación muchos a muchos administrada por EF Core
        public List<Tecnologia> Tecnologias { get; set; } = new();
    }
}