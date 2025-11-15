using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo
{
    [Table("permiso")]
    public class Permiso
    {
        [Key, Column("chr_emplcodigo", Order = 0)]
        [StringLength(4)]
        public string ChrEmplcodigo { get; set; } = string.Empty;

        [Key, Column("int_moducodigo", Order = 1)]
        public int IntModucodigo { get; set; }

        [Column("vch_permestado")]
        [StringLength(15)]
        public string VchPermestado { get; set; } = "ACTIVO";

        // Relaciones
        [ForeignKey("ChrEmplcodigo")]
        public Usuario? Usuario { get; set; }

        [ForeignKey("IntModucodigo")]
        public Modulo? Modulo { get; set; }
    }
}