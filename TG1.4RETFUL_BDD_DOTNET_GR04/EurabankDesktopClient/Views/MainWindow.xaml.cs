using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using EurabankDesktopClient.ViewModels;

namespace EurabankDesktopClient.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            // Obtener el ViewModel del Service Provider
            var serviceProvider = ((App)Application.Current).ServiceProvider;
            DataContext = serviceProvider.GetRequiredService<MainViewModel>();
        }
    }
}
