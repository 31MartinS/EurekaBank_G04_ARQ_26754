using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo
{
    [Table("asignado")]
    public class Asignado
    {
        [Key]
        [Column("chr_asigcodigo")]
        [StringLength(6)]
        public string ChrAsigcodigo { get; set; } = string.Empty;

        [Column("chr_sucucodigo")]
        [StringLength(3)]
        public string ChrSucucodigo { get; set; } = string.Empty;

        [Column("chr_emplcodigo")]
        [StringLength(4)]
        public string ChrEmplcodigo { get; set; } = string.Empty;

        [Column("dtt_asigfechaalta")]
        public DateTime DttAsigfechaalta { get; set; }

        [Column("dtt_asigfechabaja")]
        public DateTime? DttAsigfechabaja { get; set; }

        // Relaciones
        [ForeignKey("ChrSucucodigo")]
        public Sucursal? Sucursal { get; set; }

        [ForeignKey("ChrEmplcodigo")]
        public Empleado? Empleado { get; set; }
    }
}