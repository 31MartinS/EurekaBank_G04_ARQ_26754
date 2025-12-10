using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EurabankMobileClient.Models;
using EurabankMobileClient.Services;
using EurabankMobileClient.Views;

namespace EurabankMobileClient.ViewModels
{
    public partial class ClientesViewModel : BaseViewModel
    {
        private readonly EurabankApiService _apiService;

        [ObservableProperty]
        private ObservableCollection<ClienteAgrupado> clientes = new();

        public ClientesViewModel(EurabankApiService apiService)
        {
            _apiService = apiService;
            Title = "Lista de Clientes";
        }

        [RelayCommand]
        async Task CargarClientes()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                Clientes.Clear();

                var cuentas = await _apiService.ObtenerTodasLasCuentasAsync();

                var clientesAgrupados = cuentas
                    .GroupBy(c => c.CodigoCliente)
                    .Select(g => new ClienteAgrupado
                    {
                        Codigo = g.Key,
                        Nombre = g.First().NombreCliente,
                        TotalCuentas = g.Count(),
                        SaldoTotal = g.Sum(c => c.Saldo)
                    })
                    .OrderBy(c => c.Codigo);

                foreach (var cliente in clientesAgrupados)
                {
                    Clientes.Add(cliente);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"No se pudo cargar clientes: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task SeleccionarOperacion(ClienteAgrupado cliente)
        {
            var accion = await Shell.Current.DisplayActionSheet(
                $"Cliente: {cliente.Nombre}",
                "Cancelar",
                null,
                "Realizar DepÃ³sito",
                "Realizar Retiro",
                "Realizar Transferencia",
                "Ver Movimientos"
            );

            if (accion == "Realizar DepÃ³sito")
            {
                await Shell.Current.GoToAsync($"{nameof(DepositoPage)}?Codigo={cliente.Codigo}&Nombre={Uri.EscapeDataString(cliente.Nombre)}");
            }
            else if (accion == "Realizar Retiro")
            {
                await Shell.Current.GoToAsync($"{nameof(RetiroPage)}?Codigo={cliente.Codigo}&Nombre={Uri.EscapeDataString(cliente.Nombre)}");
            }
            else if (accion == "Realizar Transferencia")
            {
                await Shell.Current.GoToAsync($"{nameof(TransferenciaPage)}?CodigoOrigen={cliente.Codigo}&NombreOrigen={Uri.EscapeDataString(cliente.Nombre)}");
            }
            else if (accion == "Ver Movimientos")
            {
                await Shell.Current.GoToAsync($"{nameof(MovimientosPage)}?Codigo={cliente.Codigo}&Nombre={Uri.EscapeDataString(cliente.Nombre)}");
            }
        }


        [RelayCommand]
        async Task IrADeposito(ClienteAgrupado cliente)
        {
            await Shell.Current.GoToAsync($"{nameof(DepositoPage)}?Codigo={cliente.Codigo}&Nombre={Uri.EscapeDataString(cliente.Nombre)}");
        }

        [RelayCommand]
        async Task IrARetiro(ClienteAgrupado cliente)
        {
            await Shell.Current.GoToAsync($"{nameof(RetiroPage)}?Codigo={cliente.Codigo}&Nombre={Uri.EscapeDataString(cliente.Nombre)}");
        }

        [RelayCommand]
        async Task IrATransferencia(ClienteAgrupado cliente)
        {
            await Shell.Current.GoToAsync($"{nameof(TransferenciaPage)}?CodigoOrigen={cliente.Codigo}&NombreOrigen={Uri.EscapeDataString(cliente.Nombre)}");
        }

        [RelayCommand]
        async Task IrAMovimientos(ClienteAgrupado cliente)
        {
            await Shell.Current.GoToAsync($"{nameof(MovimientosPage)}?Codigo={cliente.Codigo}&Nombre={Uri.EscapeDataString(cliente.Nombre)}");
        }
        [RelayCommand]
        async Task Cerrar()
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }

    public class ClienteAgrupado
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int TotalCuentas { get; set; }
        public decimal SaldoTotal { get; set; }
        public string SaldoTexto => $"Saldo: {SaldoTotal:C}";
        public string CuentasTexto => $"{TotalCuentas} cuenta{(TotalCuentas != 1 ? "s" : "")}";
    }
}

