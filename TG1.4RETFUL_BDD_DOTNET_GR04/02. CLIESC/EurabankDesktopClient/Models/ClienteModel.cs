using System.ComponentModel;

namespace EurabankDesktopClient.Models
{
    public class ClienteModel : INotifyPropertyChanged
    {
        private string _codigo = string.Empty;
        private string _nombre = string.Empty;
        private string _apellido = string.Empty;
        private decimal _saldo;

        public string Codigo
        {
            get => _codigo;
            set { _codigo = value; OnPropertyChanged(nameof(Codigo)); }
        }

        public string Nombre
        {
            get => _nombre;
            set { _nombre = value; OnPropertyChanged(nameof(Nombre)); }
        }

        public string Apellido
        {
            get => _apellido;
            set { _apellido = value; OnPropertyChanged(nameof(Apellido)); }
        }

        public decimal Saldo
        {
            get => _saldo;
            set { _saldo = value; OnPropertyChanged(nameof(Saldo)); }
        }

        public string NombreCompleto => $"{Nombre} {Apellido}".Trim();
        public string SaldoFormateado => $"${Saldo:N2}";

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class MovimientoModel : INotifyPropertyChanged
    {
        private DateTime _fecha;
        private string _nombreCliente = string.Empty;
        private string _tipoMovimiento = string.Empty;
        private decimal _monto;
        private decimal _saldo;

        public DateTime Fecha
        {
            get => _fecha;
            set { _fecha = value; OnPropertyChanged(nameof(Fecha)); OnPropertyChanged(nameof(FechaFormateada)); }
        }

        public string NombreCliente
        {
            get => _nombreCliente;
            set { _nombreCliente = value; OnPropertyChanged(nameof(NombreCliente)); }
        }

        public string TipoMovimiento
        {
            get => _tipoMovimiento;
            set { _tipoMovimiento = value; OnPropertyChanged(nameof(TipoMovimiento)); }
        }

        public decimal Monto
        {
            get => _monto;
            set { _monto = value; OnPropertyChanged(nameof(Monto)); OnPropertyChanged(nameof(MontoFormateado)); }
        }

        public decimal Saldo
        {
            get => _saldo;
            set { _saldo = value; OnPropertyChanged(nameof(Saldo)); OnPropertyChanged(nameof(SaldoFormateado)); }
        }

        public string FechaFormateada => Fecha.ToString("dd/MM/yyyy HH:mm");
        public string MontoFormateado => $"${Monto:N2}";
        public string SaldoFormateado => $"${Saldo:N2}";

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
