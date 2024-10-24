using System.ComponentModel.DataAnnotations.Schema;

namespace Viajes.Models
{
    [Table("t_categoria")]
    public class Categoria
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string? Nombre { get; set; }
    }
}