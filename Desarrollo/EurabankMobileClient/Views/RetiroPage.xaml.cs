using EurabankMobileClient.ViewModels;

namespace EurabankMobileClient.Views;

public partial class RetiroPage : ContentPage
{
    public RetiroPage(RetiroViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
