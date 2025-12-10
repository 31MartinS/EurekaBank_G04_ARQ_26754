using EurabankMobileClient.ViewModels;

namespace EurabankMobileClient.Views;

public partial class ClientesPage : ContentPage
{
    private readonly ClientesViewModel _viewModel;

    public ClientesPage(ClientesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.CargarClientesCommand.Execute(null);
    }
}
