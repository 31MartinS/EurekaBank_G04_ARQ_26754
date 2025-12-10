using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EurabankMobileClient.Models;
using EurabankMobileClient.Services;

namespace EurabankMobileClient.ViewModels
{
    [QueryProperty(nameof(CodigoCliente), "Codigo")]
    [QueryProperty(nameof(NombreCliente), "Nombre")]
    public partial class DepositoViewModel : BaseViewModel
    {
        private readonly EurabankApiService _apiService;

        [ObservableProperty]
        private string codigoCliente = string.Empty;

        [ObservableProperty]
        private string nombreCliente = string.Empty;

        [ObservableProperty]
        private string codigoCuenta = string.Empty;

        [ObservableProperty]
        private decimal saldoActual;

        [ObservableProperty]
        private string montoTexto = string.Empty;

        public DepositoViewModel(EurabankApiService apiService)
        {
            _apiService = apiService;
            Title = "Realizar Depósito";
        }

        partial void OnCodigoClienteChanged(string value)
        {
            CargarCuentaAsync().ConfigureAwait(false);
        }

        async Task CargarCuentaAsync()
        {
            try
            {
                var cuentas = await _apiService.ObtenerTodasLasCuentasAsync();
                var cuenta = cuentas.FirstOrDefault(c => c.CodigoCliente == CodigoCliente);
                
                if (cuenta != null)
                {
                    CodigoCuenta = cuenta.Codigo;
                    SaldoActual = cuenta.Saldo;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"No se pudo cargar la cuenta: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        async Task RealizarDeposito()
        {
            if (IsBusy)
                return;

            if (!decimal.TryParse(MontoTexto, out decimal monto) || monto <= 0)
            {
                await Shell.Current.DisplayAlert("Error", "Por favor ingrese un monto válido mayor a 0", "OK");
                return;
            }

            try
            {
                IsBusy = true;

                var dto = new RealizarMovimientoDto
                {
                    CodigoCuenta = CodigoCuenta,
                    CodigoEmpleado = "0001",
                    CodigoTipo = "003",
                    Importe = monto
                };

                await _apiService.RealizarDepositoAsync(dto);

                await Shell.Current.DisplayAlert("Éxito", $"Depósito de {monto:C} realizado correctamente", "OK");
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"No se pudo realizar el depósito: {ex.Message}", "OK");
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
}
