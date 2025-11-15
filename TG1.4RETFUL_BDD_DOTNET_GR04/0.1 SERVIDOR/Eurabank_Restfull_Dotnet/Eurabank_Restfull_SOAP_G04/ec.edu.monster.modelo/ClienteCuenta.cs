namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo
{
    // Tabla intermedia: Cliente - Cuenta (relación muchos a muchos)
    public class ClienteCuenta
    {
        public int IdClientecuenta { get; set; }
        public int IdCliente { get; set; }
        public int IdCuenta { get; set; }
        public string CcTipo { get; set; } = "TITULAR"; // TITULAR, AUTORIZADO
        public DateTime CcFechaAsignacion { get; set; } = DateTime.Now;
        
        // Relaciones
        public Cliente? Cliente { get; set; }
        public Cuenta? Cuenta { get; set; }
    }
}
