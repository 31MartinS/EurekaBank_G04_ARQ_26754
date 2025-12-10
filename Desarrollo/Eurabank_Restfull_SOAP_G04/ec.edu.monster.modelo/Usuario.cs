using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo
{
    [Table("usuario")]
    public class Usuario
    {
        [Key]
        [Column("chr_emplcodigo")]
        [StringLength(4)]
        public string ChrEmplcodigo { get; set; } = string.Empty;

        [Column("vch_emplusuario")]
        [StringLength(20)]
        public string VchEmplusuario { get; set; } = string.Empty;

        [Column("vch_emplclave")]
        [StringLength(50)]
        public string VchEmplclave { get; set; } = string.Empty;

        [Column("vch_emplestado")]
        [StringLength(15)]
        public string VchEmplestado { get; set; } = "ACTIVO";

        // Relaciones
        [ForeignKey("ChrEmplcodigo")]
        public Empleado? Empleado { get; set; }

        public ICollection<Permiso>? Permisos { get; set; }
    }
}
