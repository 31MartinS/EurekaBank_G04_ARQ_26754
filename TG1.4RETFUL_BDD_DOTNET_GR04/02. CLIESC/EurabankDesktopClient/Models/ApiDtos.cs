namespace EurabankDesktopClient.Models
{
    public class ClienteDto
    {
        public string Codigo { get; set; } = string.Empty;
        public string Dni { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Paterno { get; set; } = string.Empty;
        public string Materno { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class CuentaDto
    {
        public string Codigo { get; set; } = string.Empty;
        public string CodigoCliente { get; set; } = string.Empty;
        public string NombreCliente { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
    }

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

    public class TipoMovimientoDto
    {
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Accion { get; set; } = string.Empty;
    }
}
