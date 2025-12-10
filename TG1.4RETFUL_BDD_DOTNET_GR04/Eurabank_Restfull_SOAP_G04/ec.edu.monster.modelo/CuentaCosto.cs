namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo
{
    // Costos aplicados a cada cuenta
    public class CuentaCosto
    {
        public int IdCuentacosto { get; set; }
        public int IdCuenta { get; set; }
        public int? IdCargomantenimiento { get; set; }
        public int? IdCostomovimiento { get; set; }
        public DateTime CcFechaAplicacion { get; set; } = DateTime.Now;
        public decimal CcMonto { get; set; }
        
        // Relaciones
        public Cuenta? Cuenta { get; set; }
        public CargoMantenimiento? CargoMantenimiento { get; set; }
        public CostoMovimiento? CostoMovimiento { get; set; }
    }
}
