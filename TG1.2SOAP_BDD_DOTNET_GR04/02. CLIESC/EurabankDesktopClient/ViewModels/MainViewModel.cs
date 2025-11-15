using System.Collections.ObjectModel;
using System.Windows;
using EurabankDesktopClient.Models;
using EurabankDesktopClient.Services;

namespace EurabankDesktopClient.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly EurabankApiService _apiService;
        private ObservableCollection<ClienteModel> _clientes = new();
        private ObservableCollection<MovimientoModel> _movimientos = new();
        private ClienteModel? _clienteSeleccionado;
        private string _currentView = "Lista";
        private string _mensaje = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _isLoading;

        // Propiedades para operaciones
        private decimal _monto;
        private string _tipoOperacion = string.Empty;
        private ClienteModel? _clienteDestino;
        private ObservableCollection<ClienteModel> _clientesDisponibles = new();

        public ObservableCollection<ClienteModel> Clientes
        {
            get => _clientes;
            set => SetProperty(ref _clientes, value);
        }

        public ObservableCollection<MovimientoModel> Movimientos
        {
            get => _movimientos;
            set => SetProperty(ref _movimientos, value);
        }

        public ClienteModel? ClienteSeleccionado
        {
            get => _clienteSeleccionado;
            set => SetProperty(ref _clienteSeleccionado, value);
        }

        public string CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        public string Mensaje
        {
            get => _mensaje;
            set => SetProperty(ref _mensaje, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public decimal Monto
        {
            get => _monto;
            set => SetProperty(ref _monto, value);
        }

        public string TipoOperacion
        {
            get => _tipoOperacion;
            set => SetProperty(ref _tipoOperacion, value);
        }

        public ClienteModel? ClienteDestino
        {
            get => _clienteDestino;
            set => SetProperty(ref _clienteDestino, value);
        }

        public ObservableCollection<ClienteModel> ClientesDisponibles
        {
            get => _clientesDisponibles;
            set => SetProperty(ref _clientesDisponibles, value);
        }

        public RelayCommand CargarClientesCommand { get; }
        public RelayCommand DepositoCommand { get; }
        public RelayCommand RetiroCommand { get; }
        public RelayCommand TransferenciaCommand { get; }
        public RelayCommand VerMovimientosCommand { get; }
        public RelayCommand RealizarDepositoCommand { get; }
        public RelayCommand RealizarRetiroCommand { get; }
        public RelayCommand RealizarTransferenciaCommand { get; }
        public RelayCommand VolverCommand { get; }
        public RelayCommand ActualizarCommand { get; }

        public MainViewModel(EurabankApiService apiService)
        {
            _apiService = apiService;

            CargarClientesCommand = new RelayCommand(async _ => await CargarClientes());
            DepositoCommand = new RelayCommand(MostrarDeposito, p => p is ClienteModel);
            RetiroCommand = new RelayCommand(MostrarRetiro, p => p is ClienteModel);
            TransferenciaCommand = new RelayCommand(MostrarTransferencia, p => p is ClienteModel);
            VerMovimientosCommand = new RelayCommand(async _ => await MostrarMovimientos());
            RealizarDepositoCommand = new RelayCommand(async _ => await RealizarDeposito(), _ => Monto > 0);
            RealizarRetiroCommand = new RelayCommand(async _ => await RealizarRetiro(), _ => Monto > 0);
            RealizarTransferenciaCommand = new RelayCommand(async _ => await RealizarTransferencia(), _ => Monto > 0 && ClienteDestino != null);
            VolverCommand = new RelayCommand(_ => Volver());
            ActualizarCommand = new RelayCommand(async _ => await ActualizarDatos());

            _ = CargarClientes();
        }

        private async Task CargarClientes()
        {
            try
            {
                IsLoading = true;
                ErrorMessage = string.Empty;

                var cuentas = await _apiService.ObtenerTodasLasCuentasAsync();
                
                var clientesAgrupados = cuentas
                    .GroupBy(c => c.CodigoCliente)
                    .Select(g => new ClienteModel
                    {
                        Codigo = g.First().CodigoCliente,
                        Nombre = g.First().NombreCliente.Split(' ').FirstOrDefault() ?? "",
                        Apellido = string.Join(" ", g.First().NombreCliente.Split(' ').Skip(1)),
                        Saldo = g.Sum(c => c.Saldo)
                    })
                    .ToList();

                Clientes.Clear();
                foreach (var cliente in clientesAgrupados)
                {
                    Clientes.Add(cliente);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al cargar clientes: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void MostrarDeposito(object? parameter)
        {
            if (parameter is ClienteModel cliente)
            {
                ClienteSeleccionado = cliente;
            }
            TipoOperacion = "DEPOSITO";
            Monto = 0;
            Mensaje = string.Empty;
            ErrorMessage = string.Empty;
            CurrentView = "DepositoRetiro";
        }

        private void MostrarRetiro(object? parameter)
        {
            if (parameter is ClienteModel cliente)
            {
                ClienteSeleccionado = cliente;
            }
            TipoOperacion = "RETIRO";
            Monto = 0;
            Mensaje = string.Empty;
            ErrorMessage = string.Empty;
            CurrentView = "DepositoRetiro";
        }

        private void MostrarTransferencia(object? parameter)
        {
            if (parameter is ClienteModel cliente)
            {
                ClienteSeleccionado = cliente;
            }
            
            TipoOperacion = "TRANSFERENCIA";
            Monto = 0;
            ClienteDestino = null;
            Mensaje = string.Empty;
            ErrorMessage = string.Empty;

            // Cargar clientes disponibles (excluyendo el origen)
            ClientesDisponibles.Clear();
            foreach (var c in Clientes.Where(c => c.Codigo != ClienteSeleccionado?.Codigo))
            {
                ClientesDisponibles.Add(c);
            }

            CurrentView = "Transferencia";
        }

        private async Task MostrarMovimientos()
        {
            try
            {
                IsLoading = true;
                ErrorMessage = string.Empty;
                Mensaje = string.Empty;

                var cuentas = await _apiService.ObtenerTodasLasCuentasAsync();
                var todosMovimientos = new List<MovimientoDto>();

                foreach (var cuenta in cuentas)
                {
                    try
                    {
                        var movs = await _apiService.ObtenerMovimientosPorCuentaAsync(cuenta.Codigo);
                        todosMovimientos.AddRange(movs);
                    }
                    catch { continue; }
                }

                Movimientos.Clear();
                decimal saldoAcum = 0;
                foreach (var mov in todosMovimientos.OrderBy(m => m.Fecha))
                {
                    var cuenta = cuentas.FirstOrDefault(c => c.Codigo == mov.CodigoCuenta);
                    
                    if (mov.CodigoTipo == "003" || mov.CodigoTipo == "005" || mov.CodigoTipo == "008")
                        saldoAcum += mov.Importe;
                    else
                        saldoAcum -= mov.Importe;

                    Movimientos.Add(new MovimientoModel
                    {
                        Fecha = mov.Fecha,
                        NombreCliente = cuenta?.NombreCliente ?? "Desconocido",
                        TipoMovimiento = mov.NombreTipo,
                        Monto = mov.Importe,
                        Saldo = saldoAcum
                    });
                }

                CurrentView = "Movimientos";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al cargar movimientos: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task RealizarDeposito()
        {
            try
            {
                IsLoading = true;
                ErrorMessage = string.Empty;

                var cuentas = await _apiService.ObtenerTodasLasCuentasAsync();
                var cuenta = cuentas.FirstOrDefault(c => c.CodigoCliente == ClienteSeleccionado?.Codigo);

                if (cuenta == null)
                {
                    ErrorMessage = "No se encontró cuenta para este cliente";
                    return;
                }

                var dto = new RealizarMovimientoDto
                {
                    CodigoCuenta = cuenta.Codigo,
                    CodigoEmpleado = "0001",
                    CodigoTipo = "003",
                    Importe = Monto
                };

                await _apiService.RealizarDepositoAsync(dto);
                
                Mensaje = $"Depósito de ${Monto:N2} realizado con éxito";
                await Task.Delay(2000);
                await ActualizarDatos();
                Volver();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task RealizarRetiro()
        {
            try
            {
                IsLoading = true;
                ErrorMessage = string.Empty;

                var cuentas = await _apiService.ObtenerTodasLasCuentasAsync();
                var cuenta = cuentas.FirstOrDefault(c => c.CodigoCliente == ClienteSeleccionado?.Codigo);

                if (cuenta == null)
                {
                    ErrorMessage = "No se encontró cuenta para este cliente";
                    return;
                }

                var dto = new RealizarMovimientoDto
                {
                    CodigoCuenta = cuenta.Codigo,
                    CodigoEmpleado = "0001",
                    CodigoTipo = "004",
                    Importe = Monto
                };

                await _apiService.RealizarRetiroAsync(dto);
                
                Mensaje = $"Retiro de ${Monto:N2} realizado con éxito";
                await Task.Delay(2000);
                await ActualizarDatos();
                Volver();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task RealizarTransferencia()
        {
            try
            {
                IsLoading = true;
                ErrorMessage = string.Empty;

                var cuentas = await _apiService.ObtenerTodasLasCuentasAsync();
                var cuentaOrigen = cuentas.FirstOrDefault(c => c.CodigoCliente == ClienteSeleccionado?.Codigo);
                var cuentaDestino = cuentas.FirstOrDefault(c => c.CodigoCliente == ClienteDestino?.Codigo);

                if (cuentaOrigen == null || cuentaDestino == null)
                {
                    ErrorMessage = "No se encontraron las cuentas";
                    return;
                }

                var dto = new RealizarTransferenciaDto
                {
                    CodigoCuentaOrigen = cuentaOrigen.Codigo,
                    CodigoCuentaDestino = cuentaDestino.Codigo,
                    CodigoEmpleado = "0001",
                    CodigoTipo = "009",
                    Importe = Monto
                };

                await _apiService.RealizarTransferenciaAsync(dto);
                
                Mensaje = $"Transferencia de ${Monto:N2} realizada con éxito";
                await Task.Delay(2000);
                await ActualizarDatos();
                Volver();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void Volver()
        {
            CurrentView = "Lista";
            Mensaje = string.Empty;
            ErrorMessage = string.Empty;
            Monto = 0;
            ClienteDestino = null;
        }

        private async Task ActualizarDatos()
        {
            await CargarClientes();
        }
    }
}
