using Microsoft.Extensions.Logging;
using EurabankMobileClient.Services;
using EurabankMobileClient.ViewModels;
using EurabankMobileClient.Views;

namespace EurabankMobileClient;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // Registrar Servicios
        builder.Services.AddSingleton<EurabankApiService>();

        // Registrar ViewModels
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<ClientesViewModel>();
        builder.Services.AddTransient<DepositoViewModel>();
        builder.Services.AddTransient<RetiroViewModel>();
        builder.Services.AddTransient<TransferenciaViewModel>();
        builder.Services.AddTransient<MovimientosViewModel>();

        // Registrar Views
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<ClientesPage>();
        builder.Services.AddTransient<DepositoPage>();
        builder.Services.AddTransient<RetiroPage>();
        builder.Services.AddTransient<TransferenciaPage>();
        builder.Services.AddTransient<MovimientosPage>();

        return builder.Build();
    }
}
