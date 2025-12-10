using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using EurabankDesktopClient.Services;
using EurabankDesktopClient.ViewModels;
using EurabankDesktopClient.Views;

namespace EurabankDesktopClient
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Configurar HttpClient
            services.AddHttpClient<EurabankApiService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5199");
                client.Timeout = TimeSpan.FromSeconds(30);
            });

            // Registrar ViewModels
            services.AddTransient<MainViewModel>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var loginWindow = new LoginWindow();
            loginWindow.Show();
        }
    }
}
