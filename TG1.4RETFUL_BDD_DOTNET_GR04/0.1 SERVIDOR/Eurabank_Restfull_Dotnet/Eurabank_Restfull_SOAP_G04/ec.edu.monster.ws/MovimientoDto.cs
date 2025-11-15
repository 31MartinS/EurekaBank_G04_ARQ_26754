namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.ws
{
    public class MovimientoDto
    {
        public string CodigoCuenta { get; set; } = string.Empty;
        public int NumeroMovimiento { get; set; }
        public DateTime Fecha { get; set; }
        public string CodigoTipo { get; set; } = string.Empty;
        public string NombreTipo { get; set; } = string.Empty;
        public decimal Importe { get; set; }
        public string? CuentaReferencia { get; set; }
    }

    public class RealizarMovimientoDto
    {
        public string CodigoCuenta { get; set; } = string.Empty;
        public string CodigoEmpleado { get; set; } = string.Empty;
        public string CodigoTipo { get; set; } = string.Empty;
        public decimal Importe { get; set; }
        public string? CuentaReferencia { get; set; }
    }

    public class RealizarTransferenciaDto
    {
        public string CodigoCuentaOrigen { get; set; } = string.Empty;
        public string CodigoCuentaDestino { get; set; } = string.Empty;
        public string CodigoEmpleado { get; set; } = string.Empty;
        public string CodigoTipo { get; set; } = string.Empty;
        public decimal Importe { get; set; }
    }
}
