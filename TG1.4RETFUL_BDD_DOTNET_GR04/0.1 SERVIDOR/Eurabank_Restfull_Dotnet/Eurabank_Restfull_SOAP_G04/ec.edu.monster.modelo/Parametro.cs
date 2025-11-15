using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo
{
    [Table("parametro")]
    public class Parametro
    {
        [Key]
        [Column("chr_paracodigo")]
        [StringLength(3)]
        public string ChrParacodigo { get; set; } = string.Empty;

        [Column("vch_paradescripcion")]
        [StringLength(50)]
        public string VchParadescripcion { get; set; } = string.Empty;

        [Column("vch_paravalor")]
        [StringLength(70)]
        public string VchParavalor { get; set; } = string.Empty;

        [Column("vch_paraestado")]
        [StringLength(15)]
        public string VchParaestado { get; set; } = "ACTIVO";
    }
}