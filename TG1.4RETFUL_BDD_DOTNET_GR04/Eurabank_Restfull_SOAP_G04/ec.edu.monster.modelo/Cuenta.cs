using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo
{
    [Table("cuenta")]
    public class Cuenta
    {
        [Key]
        [Column("chr_cuencodigo")]
        [StringLength(8)]
        public string ChrCuencodigo { get; set; } = string.Empty;

        [Column("chr_monecodigo")]
        [StringLength(2)]
        public string ChrMonecodigo { get; set; } = string.Empty;

        [Column("chr_sucucodigo")]
        [StringLength(3)]
        public string ChrSucucodigo { get; set; } = string.Empty;

        [Column("chr_emplcreacuenta")]
        [StringLength(4)]
        public string ChrEmplcreacuenta { get; set; } = string.Empty;

        [Column("chr_cliecodigo")]
        [StringLength(5)]
        public string ChrCliecodigo { get; set; } = string.Empty;

        [Column("dec_cuensaldo", TypeName = "decimal(12,2)")]
        public decimal DecCuensaldo { get; set; }

        [Column("dtt_cuenfechacreacion")]
        public DateTime DttCuenfechacreacion { get; set; }

        [Column("vch_cuenestado")]
        [StringLength(15)]
        public string VchCuenestado { get; set; } = "ACTIVO";

        [Column("int_cuencontmov")]
        public int IntCuencontmov { get; set; }

        [Column("chr_cuenclave")]
        [StringLength(6)]
        public string ChrCuenclave { get; set; } = string.Empty;

        // Relaciones
        [ForeignKey("ChrMonecodigo")]
        public Modena? Modena { get; set; }

        [ForeignKey("ChrSucucodigo")]
        public Sucursal? Sucursal { get; set; }

        [ForeignKey("ChrEmplcreacuenta")]
        public Empleado? EmpleadoCreador { get; set; }

        [ForeignKey("ChrCliecodigo")]
        public Cliente? Cliente { get; set; }

        public ICollection<Movimiento>? Movimientos { get; set; }
    }
}
