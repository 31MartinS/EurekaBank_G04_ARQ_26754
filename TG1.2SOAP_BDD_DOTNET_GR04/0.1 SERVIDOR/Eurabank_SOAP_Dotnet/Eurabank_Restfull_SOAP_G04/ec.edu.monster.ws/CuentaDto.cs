namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.ws
{
    public class CuentaDto
    {
        public string Codigo { get; set; } = string.Empty;
        public string CodigoMoneda { get; set; } = string.Empty;
        public string NombreMoneda { get; set; } = string.Empty;
        public string CodigoSucursal { get; set; } = string.Empty;
        public string NombreSucursal { get; set; } = string.Empty;
        public string CodigoCliente { get; set; } = string.Empty;
        public string NombreCliente { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Estado { get; set; } = string.Empty;
    }

    public class CrearCuentaDto
    {
        public string CodigoMoneda { get; set; } = string.Empty;
        public string CodigoSucursal { get; set; } = string.Empty;
        public string CodigoEmpleado { get; set; } = string.Empty;
        public string CodigoCliente { get; set; } = string.Empty;
    }
}
