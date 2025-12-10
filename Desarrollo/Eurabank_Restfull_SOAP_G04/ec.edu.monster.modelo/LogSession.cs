using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo
{
    [Table("LOGSESSION")]
    public class LogSession
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column("chr_emplcodigo")]
        [StringLength(4)]
        public string ChrEmplcodigo { get; set; } = string.Empty;

        [Column("fec_ingreso")]
        public DateTime FecIngreso { get; set; }

        [Column("fec_salida")]
        public DateTime? FecSalida { get; set; }

        [Column("ip")]
        [StringLength(20)]
        public string Ip { get; set; } = "NONE";

        [Column("hostname")]
        [StringLength(100)]
        public string Hostname { get; set; } = "NONE";

        // Relaciones
        [ForeignKey("ChrEmplcodigo")]
        public Empleado? Empleado { get; set; }
    }
}