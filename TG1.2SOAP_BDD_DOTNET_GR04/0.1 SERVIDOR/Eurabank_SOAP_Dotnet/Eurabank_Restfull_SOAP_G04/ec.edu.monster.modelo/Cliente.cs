using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo
{
    [Table("cliente")]
    public class Cliente
    {
        [Key]
        [Column("chr_cliecodigo")]
        [StringLength(5)]
        public string ChrCliecodigo { get; set; } = string.Empty;

        [Column("vch_cliepaterno")]
        [StringLength(25)]
        public string VchCliepaterno { get; set; } = string.Empty;

        [Column("vch_cliematerno")]
        [StringLength(25)]
        public string VchCliematerno { get; set; } = string.Empty;

        [Column("vch_clienombre")]
        [StringLength(30)]
        public string VchClienombre { get; set; } = string.Empty;

        [Column("chr_cliedni")]
        [StringLength(8)]
        public string ChrCliedni { get; set; } = string.Empty;

        [Column("vch_clieciudad")]
        [StringLength(30)]
        public string VchClieciudad { get; set; } = string.Empty;

        [Column("vch_cliedireccion")]
        [StringLength(50)]
        public string VchCliedireccion { get; set; } = string.Empty;

        [Column("vch_clietelefono")]
        [StringLength(20)]
        public string? VchClietelefono { get; set; }

        [Column("vch_clieemail")]
        [StringLength(50)]
        public string? VchClieemail { get; set; }

        // Relaciones
        public ICollection<Cuenta>? Cuentas { get; set; }
    }
}
