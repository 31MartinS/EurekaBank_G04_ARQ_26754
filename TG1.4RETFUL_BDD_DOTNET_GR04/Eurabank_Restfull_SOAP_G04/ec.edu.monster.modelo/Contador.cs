using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo
{
    [Table("contador")]
    public class Contador
    {
        [Key]
        [Column("vch_conttabla")]
        [StringLength(30)]
        public string VchConttabla { get; set; } = string.Empty;

        [Column("int_contitem")]
        public int IntContitem { get; set; }

        [Column("int_contlongitud")]
        public int IntContlongitud { get; set; }
    }
}
