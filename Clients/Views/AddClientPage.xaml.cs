using Clients.ViewModel;

namespace Clients.Views;

public partial class AddClientPage : ContentPage
{
	public AddClientPage(AddClientViewModel viewModel)
	{
        viewModel.NavigateBackRequested += OnNavigateBackRequested;

		BindingContext = viewModel;

		InitializeComponent();
	}

    private async void OnNavigateBackRequested(object? sender, EventArgs e)
    {
	    await Navigation.PopAsync();
    }
}