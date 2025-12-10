using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EurabankMobileClient.Models;
using EurabankMobileClient.Services;

namespace EurabankMobileClient.ViewModels
{
    [QueryProperty(nameof(CodigoCliente), "Codigo")]
    [QueryProperty(nameof(NombreCliente), "Nombre")]
    public partial class MovimientosViewModel : BaseViewModel
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
        private ObservableCollection<MovimientoDto> movimientos = new();

        public MovimientosViewModel(EurabankApiService apiService)
        {
            _apiService = apiService;
            Title = "Movimientos";
        }

        partial void OnCodigoClienteChanged(string value)
        {
            CargarMovimientosAsync().ConfigureAwait(false);
        }

        async Task CargarMovimientosAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                var cuentas = await _apiService.ObtenerTodasLasCuentasAsync();
                var cuenta = cuentas.FirstOrDefault(c => c.CodigoCliente == CodigoCliente);

                if (cuenta != null)
                {
                    CodigoCuenta = cuenta.Codigo;
                    SaldoActual = cuenta.Saldo;

                    var movs = await _apiService.ObtenerMovimientosPorCuentaAsync(cuenta.Codigo);
                    
                    Movimientos.Clear();
                    foreach (var mov in movs.OrderByDescending(m => m.Fecha))
                    {
                        Movimientos.Add(mov);
                    }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"No se pudo cargar movimientos: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task Actualizar()
        {
            await CargarMovimientosAsync();
        }

        [RelayCommand]
        async Task Volver()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
