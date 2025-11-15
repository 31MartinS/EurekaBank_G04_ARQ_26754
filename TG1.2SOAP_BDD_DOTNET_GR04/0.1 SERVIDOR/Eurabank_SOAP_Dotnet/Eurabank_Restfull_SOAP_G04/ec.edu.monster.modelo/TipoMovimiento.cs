using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo
{
    [Table("tipomovimiento")]
    public class TipoMovimiento
    {
        [Key]
        [Column("chr_tipocodigo")]
        [StringLength(3)]
        public string ChrTipocodigo { get; set; } = string.Empty;

        [Column("vch_tipodescripcion")]
        [StringLength(40)]
        public string VchTipodescripcion { get; set; } = string.Empty;

        [Column("vch_tipoaccion")]
        [StringLength(10)]
        public string VchTipoaccion { get; set; } = string.Empty; // INGRESO / SALIDA

        [Column("vch_tipoestado")]
        [StringLength(15)]
        public string VchTipoestado { get; set; } = "ACTIVO";

        // Relaciones
        public ICollection<Movimiento>? Movimientos { get; set; }
    }
}
