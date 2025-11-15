using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo
{
    [Table("cargomantenimiento")]
    public class CargoMantenimiento
    {
        [Key]
        [Column("chr_monecodigo")]
        [StringLength(2)]
        public string ChrMonecodigo { get; set; } = string.Empty;

        [Column("dec_cargMontoMaximo", TypeName = "decimal(12,2)")]
        public decimal DecCargMontoMaximo { get; set; }

        [Column("dec_cargImporte", TypeName = "decimal(12,2)")]
        public decimal DecCargImporte { get; set; }

        // Relaciones
        [ForeignKey("ChrMonecodigo")]
        public Modena? Modena { get; set; }
    }
}
