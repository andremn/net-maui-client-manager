using Clients.ViewModel;
using Microsoft.Maui.Controls;

namespace Clients.Views;

public partial class AddClientPage : ContentPage
{
	public AddClientPage(AddClientViewModel viewModel)
	{
        viewModel.CloseWindowRequest += OnCloseWindowRequest;

		BindingContext = viewModel;

		InitializeComponent();
	}

    private void OnCloseWindowRequest(object? sender, EventArgs e)
    {
        Application.Current?.CloseWindow(Window);
    }
}