using Clients.ViewModel;
using Microsoft.Maui.Controls;

namespace Clients.Views;

public partial class EditClientPage : ContentPage
{
    private readonly EditClientViewModel _viewModel;

    public EditClientPage(EditClientViewModel viewModel)
	{
        viewModel.CloseWindowRequest += OnCloseWindowRequest;
        viewModel.DeleteDialogRequest += OnDeleteDialogRequest;

		BindingContext = viewModel;

		InitializeComponent();
        _viewModel = viewModel;
    }

    private void OnCloseWindowRequest(object? sender, EventArgs e)
    {
        Application.Current?.CloseWindow(Window);
    }

    private async void OnDeleteDialogRequest(object? sender, EventArgs e)
    {
        var canDelete = await(Application.Current?.MainPage?.DisplayAlert("Confirm delete", "Do you really want to delete this client?", "Yes", "No") ?? Task.FromResult(false));

        if (canDelete)
        {
            _viewModel.DeleteCommand.Execute(null);
        }
    }
}