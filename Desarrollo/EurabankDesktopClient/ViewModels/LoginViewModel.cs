using System.Windows;
using EurabankDesktopClient.Services;

namespace EurabankDesktopClient.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _usuario = string.Empty;
        private string _contraseña = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _isLoading;

        public string Usuario
        {
            get => _usuario;
            set => SetProperty(ref _usuario, value);
        }

        public string Contraseña
        {
            get => _contraseña;
            set => SetProperty(ref _contraseña, value);
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

        public RelayCommand LoginCommand { get; }

        public event EventHandler? LoginSuccessful;

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login, CanLogin);
        }

        private bool CanLogin(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(Usuario) && 
                   !string.IsNullOrWhiteSpace(Contraseña) && 
                   !IsLoading;
        }

        private void Login(object? parameter)
        {
            ErrorMessage = string.Empty;

            // Validación hardcoded como en el cliente web
            if (Usuario == "MONSTER" && Contraseña == "monster9")
            {
                LoginSuccessful?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                ErrorMessage = "Usuario o contraseña incorrectos";
            }
        }
    }
}
