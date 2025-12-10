using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo
{
    [Table("interesmensual")]
    public class InteresMensual
    {
        [Key]
        [Column("chr_monecodigo")]
        [StringLength(2)]
        public string ChrMonecodigo { get; set; } = string.Empty;

        [Column("dec_inteimporte", TypeName = "decimal(12,2)")]
        public decimal DecInteimporte { get; set; }

        // Relaciones
        [ForeignKey("ChrMonecodigo")]
        public Modena? Modena { get; set; }
    }
}