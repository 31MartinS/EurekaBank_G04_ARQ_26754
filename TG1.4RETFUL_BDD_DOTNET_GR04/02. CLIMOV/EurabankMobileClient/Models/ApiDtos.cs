namespace EurabankMobileClient.Models
{
    public class ClienteDto
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NombreCompleto => $"{Nombre} {Apellido}";
    }

    public class CuentaDto
    {
        public string Codigo { get; set; } = string.Empty;
        public string CodigoCliente { get; set; } = string.Empty;
        public string NombreCliente { get; set; } = string.Empty;
        public string CodigoMoneda { get; set; } = string.Empty;
        public string NombreMoneda { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaApertura { get; set; }
    }

    public class MovimientoDto
    {
        public int Numero { get; set; }
        public string CodigoCuenta { get; set; } = string.Empty;
        public string CodigoEmpleado { get; set; } = string.Empty;
        public string NombreEmpleado { get; set; } = string.Empty;
        public string CodigoTipo { get; set; } = string.Empty;
        public string NombreTipo { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public decimal Importe { get; set; }
        public string? CuentaReferencia { get; set; }
    }

    public class RealizarMovimientoDto
    {
        public string CodigoCuenta { get; set; } = string.Empty;
        public string CodigoEmpleado { get; set; } = "0001";
        public string CodigoTipo { get; set; } = "009";
        public decimal Importe { get; set; }
    }

    public class RealizarTransferenciaDto
    {
        public string CodigoCuentaOrigen { get; set; } = string.Empty;
        public string CodigoCuentaDestino { get; set; } = string.Empty;
        public string CodigoEmpleado { get; set; } = "0001";
        public string CodigoTipo { get; set; } = "009";
        public decimal Importe { get; set; }
    }
}
