using Clients.Views;

namespace Clients;

public partial class App : Application
{
	public App(ClientsPage clientsPage)
	{
		InitializeComponent();

		MainPage = new NavigationPage(clientsPage);
	}
}
