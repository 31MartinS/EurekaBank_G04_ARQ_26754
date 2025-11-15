namespace EurabankWebClient.Models
{
    public class LoginViewModel
    {
        public string Usuario { get; set; } = string.Empty;
        public string Contraseña { get; set; } = string.Empty;
        public string? ErrorMessage { get; set; }
    }

    public class ClienteViewModel
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
    }

    public class DepositoRetiroViewModel
    {
        public string CodigoCliente { get; set; } = string.Empty;
        public string NombreCliente { get; set; } = string.Empty;
        public string TipoOperacion { get; set; } = "DEPOSITO"; // DEPOSITO o RETIRO
        public string CodigoCuenta { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class TransferenciaViewModel
    {
        public string CodigoClienteOrigen { get; set; } = string.Empty;
        public string NombreClienteOrigen { get; set; } = string.Empty;
        public string CodigoCuentaOrigen { get; set; } = string.Empty;
        public string CodigoClienteDestino { get; set; } = string.Empty;
        public List<ClienteViewModel> ClientesDisponibles { get; set; } = new();
        public decimal Monto { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class MovimientoViewModel
    {
        public DateTime Fecha { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public string NombreCliente { get; set; } = string.Empty;
        public string TipoMovimiento { get; set; } = string.Empty;
        public string TipoAccion { get; set; } = string.Empty; // INGRESO o SALIDA
        public decimal Importe { get; set; }
        public decimal Monto { get; set; }
        public decimal Saldo { get; set; }
    }

    public class ListadoMovimientosViewModel
    {
        public List<MovimientoViewModel> Movimientos { get; set; } = new();
        public List<ClienteViewModel> ClientesDisponibles { get; set; } = new();
        public string? FiltroUsuario { get; set; }
        public string? CodigoClienteFiltro { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
    }
}
