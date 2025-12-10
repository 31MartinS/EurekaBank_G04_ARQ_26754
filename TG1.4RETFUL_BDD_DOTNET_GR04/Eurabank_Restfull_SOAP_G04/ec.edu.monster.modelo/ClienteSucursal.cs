namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo
{
    // Tabla intermedia: Cliente - Sucursal
    public class ClienteSucursal
    {
        public int IdClientesucursal { get; set; }
        public int IdCliente { get; set; }
        public int IdSucursal { get; set; }
        public DateTime CsFechaAsignacion { get; set; } = DateTime.Now;
        
        // Relaciones
        public Cliente? Cliente { get; set; }
        public Sucursal? Sucursal { get; set; }
    }
}
