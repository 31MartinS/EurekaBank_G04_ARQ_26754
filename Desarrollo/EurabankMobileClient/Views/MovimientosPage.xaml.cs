using EurabankMobileClient.ViewModels;

namespace EurabankMobileClient.Views;

public partial class MovimientosPage : ContentPage
{
    public MovimientosPage(MovimientosViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
