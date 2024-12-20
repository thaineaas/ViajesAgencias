using System.ComponentModel.DataAnnotations.Schema;

namespace Viajes.Models
{
    [Table("t_contacto")]
    public class Contacto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {   get; set;}
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; }
        public string? Sentimiento { get; set; }

        [NotMapped]
        public string? Contrasena { get; set; }
    }
}