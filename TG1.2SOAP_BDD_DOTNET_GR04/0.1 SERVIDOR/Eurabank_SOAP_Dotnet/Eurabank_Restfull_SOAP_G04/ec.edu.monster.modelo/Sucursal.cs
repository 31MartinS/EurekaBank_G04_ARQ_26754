using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo
{
    [Table("sucursal")]
    public class Sucursal
    {
        [Key]
        [Column("chr_sucucodigo")]
        [StringLength(3)]
        public string ChrSucucodigo { get; set; } = string.Empty;

        [Column("vch_sucunombre")]
        [StringLength(50)]
        public string VchSucunombre { get; set; } = string.Empty;

        [Column("vch_sucuciudad")]
        [StringLength(30)]
        public string VchSucuciudad { get; set; } = string.Empty;

        [Column("vch_sucudireccion")]
        [StringLength(50)]
        public string? VchSucudireccion { get; set; }

        [Column("int_sucucontcuenta")]
        public int IntSucucontcuenta { get; set; }

        // Relaciones
        public ICollection<Cuenta>? Cuentas { get; set; }
        public ICollection<Asignado>? Asignados { get; set; }
    }
}
