using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo
{
    [Table("costomovimiento")]
    public class CostoMovimiento
    {
        [Key]
        [Column("chr_monecodigo")]
        [StringLength(2)]
        public string ChrMonecodigo { get; set; } = string.Empty;

        [Column("dec_costimporte", TypeName = "decimal(12,2)")]
        public decimal DecCostimporte { get; set; }

        // Relaciones
        [ForeignKey("ChrMonecodigo")]
        public Modena? Modena { get; set; }
    }
}
