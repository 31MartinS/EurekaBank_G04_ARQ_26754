using System.Windows;
using System.Windows.Controls;
using EurabankDesktopClient.ViewModels;

namespace EurabankDesktopClient.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            
            if (DataContext is LoginViewModel viewModel)
            {
                viewModel.LoginSuccessful += OnLoginSuccessful;
            }

            UsuarioTextBox.Focus();
        }

        private void ContraseñaPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel)
            {
                viewModel.Contraseña = ((PasswordBox)sender).Password;
            }
        }

        private void OnLoginSuccessful(object? sender, EventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
