using System.ComponentModel.DataAnnotations.Schema;

namespace Viajes.Models
{
    [Table("t_cliente")]
    public class Cliente
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public string? DNI { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}