using Clients.ViewModel;

namespace Clients.Views;

public partial class ClientsPage : ContentPage
{
    public ClientsPage(ClientsViewModel viewModel)
    {
        BindingContext = viewModel;

        InitializeComponent();
    }
}

