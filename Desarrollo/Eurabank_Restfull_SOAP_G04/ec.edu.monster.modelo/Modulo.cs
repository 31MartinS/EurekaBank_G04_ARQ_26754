using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo
{
    [Table("modulo")]
    public class Modulo
    {
        [Key]
        [Column("int_moducodigo")]
        public int IntModucodigo { get; set; }

        [Column("vch_modunombre")]
        [StringLength(50)]
        public string? VchModunombre { get; set; }

        [Column("vch_moduestado")]
        [StringLength(15)]
        public string VchModuestado { get; set; } = "ACTIVO";

        // Relaciones
        public ICollection<Permiso>? Permisos { get; set; }
    }
}