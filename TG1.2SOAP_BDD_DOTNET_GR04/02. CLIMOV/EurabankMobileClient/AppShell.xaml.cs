using EurabankMobileClient.Views;

namespace EurabankMobileClient;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Registrar rutas de navegación
        Routing.RegisterRoute(nameof(DepositoPage), typeof(DepositoPage));
        Routing.RegisterRoute(nameof(RetiroPage), typeof(RetiroPage));
        Routing.RegisterRoute(nameof(TransferenciaPage), typeof(TransferenciaPage));
        Routing.RegisterRoute(nameof(MovimientosPage), typeof(MovimientosPage));
    }
}
