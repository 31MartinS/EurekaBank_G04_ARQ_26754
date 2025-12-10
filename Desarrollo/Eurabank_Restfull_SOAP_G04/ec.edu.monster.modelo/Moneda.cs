using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo
{
    [Table("modena")]
    public class Modena
    {
        [Key]
        [Column("chr_monecodigo")]
        [StringLength(2)]
        public string ChrMonecodigo { get; set; } = string.Empty;

        [Column("vch_monedescripcion")]
        [StringLength(20)]
        public string VchMonedescripcion { get; set; } = string.Empty;

        // Relaciones
        public ICollection<Cuenta>? Cuentas { get; set; }
        public CargoMantenimiento? CargoMantenimiento { get; set; }
        public CostoMovimiento? CostoMovimiento { get; set; }
        public InteresMensual? InteresMensual { get; set; }
    }
}
