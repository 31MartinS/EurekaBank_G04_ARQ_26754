using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EurabankMobileClient.Models;
using EurabankMobileClient.Services;

namespace EurabankMobileClient.ViewModels
{
    [QueryProperty(nameof(CodigoOrigen), "CodigoOrigen")]
    [QueryProperty(nameof(NombreOrigen), "NombreOrigen")]
    public partial class TransferenciaViewModel : BaseViewModel
    {
        private readonly EurabankApiService _apiService;

        [ObservableProperty]
        private string codigoOrigen = string.Empty;

        [ObservableProperty]
        private string nombreOrigen = string.Empty;

        [ObservableProperty]
        private string codigoCuentaOrigen = string.Empty;

        [ObservableProperty]
        private decimal saldoDisponible;

        [ObservableProperty]
        private ObservableCollection<ClienteDestino> clientesDestino = new();

        [ObservableProperty]
        private ClienteDestino? clienteSeleccionado;

        [ObservableProperty]
        private string montoTexto = string.Empty;

        public TransferenciaViewModel(EurabankApiService apiService)
        {
            _apiService = apiService;
            Title = "Realizar Transferencia";
        }

        partial void OnCodigoOrigenChanged(string value)
        {
            CargarDatosAsync().ConfigureAwait(false);
        }

        async Task CargarDatosAsync()
        {
            try
            {
                var cuentas = await _apiService.ObtenerTodasLasCuentasAsync();
                
                // Cargar cuenta origen
                var cuentaOrigen = cuentas.FirstOrDefault(c => c.CodigoCliente == CodigoOrigen);
                if (cuentaOrigen != null)
                {
                    CodigoCuentaOrigen = cuentaOrigen.Codigo;
                    SaldoDisponible = cuentaOrigen.Saldo;
                }

                // Cargar clientes destino (todos excepto el origen)
                ClientesDestino.Clear();
                var clientesAgrupados = cuentas
                    .Where(c => c.CodigoCliente != CodigoOrigen)
                    .GroupBy(c => c.CodigoCliente)
                    .Select(g => new ClienteDestino
                    {
                        Codigo = g.Key,
                        Nombre = g.First().NombreCliente,
                        CodigoCuenta = g.First().Codigo
                    })
                    .OrderBy(c => c.Nombre);

                foreach (var cliente in clientesAgrupados)
                {
                    ClientesDestino.Add(cliente);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"No se pudo cargar datos: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        async Task RealizarTransferencia()
        {
            if (IsBusy)
                return;

            if (ClienteSeleccionado == null)
            {
                await Shell.Current.DisplayAlert("Error", "Por favor seleccione un cliente destino", "OK");
                return;
            }

            if (!decimal.TryParse(MontoTexto, out decimal monto) || monto <= 0)
            {
                await Shell.Current.DisplayAlert("Error", "Por favor ingrese un monto válido mayor a 0", "OK");
                return;
            }

            if (monto > SaldoDisponible)
            {
                await Shell.Current.DisplayAlert("Error", "Saldo insuficiente", "OK");
                return;
            }

            try
            {
                IsBusy = true;

                var dto = new RealizarTransferenciaDto
                {
                    CodigoCuentaOrigen = CodigoCuentaOrigen,
                    CodigoCuentaDestino = ClienteSeleccionado.CodigoCuenta,
                    CodigoEmpleado = "0001",
                    CodigoTipo = "009",
                    Importe = monto
                };

                await _apiService.RealizarTransferenciaAsync(dto);

                await Shell.Current.DisplayAlert("Éxito", 
                    $"Transferencia de {monto:C} realizada correctamente\n" +
                    $"De: {NombreOrigen}\n" +
                    $"Para: {ClienteSeleccionado.Nombre}", "OK");
                
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"No se pudo realizar la transferencia: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task Cancelar()
        {
            await Shell.Current.GoToAsync("..");
        }
    }

    public class ClienteDestino
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string CodigoCuenta { get; set; } = string.Empty;
    }
}
