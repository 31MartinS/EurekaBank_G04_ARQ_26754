using EurabankMobileClient.ViewModels;

namespace EurabankMobileClient.Views;

public partial class DepositoPage : ContentPage
{
    public DepositoPage(DepositoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
