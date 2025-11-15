using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EurabankMobileClient.Services;
using EurabankMobileClient.Views;

namespace EurabankMobileClient.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly EurabankApiService _apiService;

        [ObservableProperty]
        private string usuario = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private string errorMessage = string.Empty;

        public LoginViewModel(EurabankApiService apiService)
        {
            _apiService = apiService;
            Title = "Login - Eurabank";
        }

        [RelayCommand]
        async Task Login()
        {
            if (IsBusy)
                return;

            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(Usuario) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Por favor ingrese usuario y contraseña";
                return;
            }

            if (Usuario != "MONSTER" || Password != "monster9")
            {
                ErrorMessage = "Credenciales incorrectas";
                return;
            }

            try
            {
                IsBusy = true;

                System.Diagnostics.Debug.WriteLine($"Intentando conectar a la API...");
                
                // Verificar conexión con el servidor
                await _apiService.ObtenerTodasLasCuentasAsync();

                System.Diagnostics.Debug.WriteLine($"Conexión exitosa, navegando a ClientesPage");
                
                // Navegar a la página principal
                await Shell.Current.GoToAsync($"///{nameof(ClientesPage)}");
            }
            catch (HttpRequestException httpEx)
            {
                System.Diagnostics.Debug.WriteLine($"HttpRequestException: {httpEx}");
                ErrorMessage = $"No se puede conectar al servidor.\nVerifique:\n- Servidor corriendo en 10.40.17.162:5199\n- Mismo WiFi\n- Firewall";
            }
            catch (TaskCanceledException)
            {
                ErrorMessage = "Tiempo de espera agotado. Verifique la conexión de red.";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception: {ex}");
                ErrorMessage = $"Error: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
