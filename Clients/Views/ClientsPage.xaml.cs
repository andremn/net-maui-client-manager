using Clients.ViewModel;

namespace Clients.Views;

public partial class ClientsPage : ContentPage
{
    private readonly ClientsViewModel _viewModel;
    
    public ClientsPage(ClientsViewModel viewModel)
    {
        _viewModel = viewModel;
        
        viewModel.NavigateToPageRequested += OnNavigateToAddClientPageRequest;

        BindingContext = viewModel;

        InitializeComponent();
        
        LoadClients();
    }

    private async void LoadClients()
    {
        await _viewModel.LoadClientsAsync();
    }

    private async void OnNavigateToAddClientPageRequest(object? sender, NavigateRequestedEventArgs e) =>
        await Navigation.PushAsync(e.TargetPage);
}