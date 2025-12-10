using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo
{
    [Table("empleado")]
    public class Empleado
    {
        [Key]
        [Column("chr_emplcodigo")]
        [StringLength(4)]
        public string ChrEmplcodigo { get; set; } = string.Empty;

        [Column("vch_emplpaterno")]
        [StringLength(25)]
        public string VchEmplpaterno { get; set; } = string.Empty;

        [Column("vch_emplmaterno")]
        [StringLength(25)]
        public string VchEmplmaterno { get; set; } = string.Empty;

        [Column("vch_emplnombre")]
        [StringLength(30)]
        public string VchEmplnombre { get; set; } = string.Empty;

        [Column("vch_emplciudad")]
        [StringLength(30)]
        public string VchEmplciudad { get; set; } = string.Empty;

        [Column("vch_empldireccion")]
        [StringLength(50)]
        public string? VchEmpldireccion { get; set; }

        // Relaciones
        public Usuario? Usuario { get; set; }
        public ICollection<Cuenta>? CuentasCreadas { get; set; }
        public ICollection<Movimiento>? Movimientos { get; set; }
        public ICollection<Asignado>? Asignaciones { get; set; }
        public ICollection<LogSession>? LogSessions { get; set; }
    }
}