using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo
{
    [Table("movimiento")]
    public class Movimiento
    {
        [Key, Column("chr_cuencodigo", Order = 0)]
        [StringLength(8)]
        public string ChrCuencodigo { get; set; } = string.Empty;

        [Key, Column("int_movinumero", Order = 1)]
        public int IntMovinumero { get; set; }

        [Column("dtt_movifecha")]
        public DateTime DttMovifecha { get; set; }

        [Column("chr_emplcodigo")]
        [StringLength(4)]
        public string ChrEmplcodigo { get; set; } = string.Empty;

        [Column("chr_tipocodigo")]
        [StringLength(3)]
        public string ChrTipocodigo { get; set; } = string.Empty;

        [Column("dec_moviimporte", TypeName = "decimal(12,2)")]
        public decimal DecMoviimporte { get; set; }

        [Column("chr_cuenreferencia")]
        [StringLength(8)]
        public string? ChrCuenreferencia { get; set; }

        // Relaciones
        [ForeignKey("ChrCuencodigo")]
        public Cuenta? Cuenta { get; set; }

        [ForeignKey("ChrEmplcodigo")]
        public Empleado? Empleado { get; set; }

        [ForeignKey("ChrTipocodigo")]
        public TipoMovimiento? TipoMovimiento { get; set; }
    }
}
