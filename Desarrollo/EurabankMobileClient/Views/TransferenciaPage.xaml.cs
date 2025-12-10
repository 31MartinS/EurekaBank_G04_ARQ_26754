using EurabankMobileClient.ViewModels;

namespace EurabankMobileClient.Views;

public partial class TransferenciaPage : ContentPage
{
    public TransferenciaPage(TransferenciaViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
