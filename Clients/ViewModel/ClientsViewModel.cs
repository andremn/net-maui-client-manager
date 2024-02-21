using Clients.Messaging;
using Clients.Model;
using Clients.Repository;
using Clients.Views;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Clients.ViewModel;

public class ClientsViewModel : BaseViewModel
{
    private ObservableCollection<Client> _clients = [];

    public ClientsViewModel(IServiceProvider serviceProvider)
    {
        var repository = serviceProvider.GetRequiredService<IClientRepository>();

        Task.Run(async () =>
        {
            var clients = await repository.GetAllAsync();

            Application.Current?.Dispatcher.Dispatch(() =>
            {
                foreach (var client in clients)
                {
                    _clients.Add(client);
                }
            });
        });

        ClientSelectedCommand = new Command(() =>
        {
            Application.Current?.OpenWindow(new Window
            {
                Page = serviceProvider.GetRequiredService<EditClientPage>(),
                Title = "Edit client"
            });

            WeakReferenceMessenger.Default.Send(new SelectedClientChangedMessage(SelectedClient));
        });

        AddClientCommand = new Command(() =>
        {
            Application.Current?.OpenWindow(new Window
            {
                Page = serviceProvider.GetRequiredService<AddClientPage>(),
                Title = "Add client"
            });
        });

        WeakReferenceMessenger.Default.Register<ClientAddedMessage>(this, (_, message) =>
        {
            _clients.Add(message.Value);
        });

        WeakReferenceMessenger.Default.Register<ClientUpdatedMessage>(this, (_, message) =>
        {
            var clientUpdated = _clients.SingleOrDefault(x => x.Id == message.Value.Id);

            if (clientUpdated is not null)
            {
                var indexOf = _clients.IndexOf(clientUpdated);

                _clients[indexOf] = message.Value;
            }
        });

        WeakReferenceMessenger.Default.Register<ClientDeletedMessage>(this, (_, message) =>
        {
            var clientDeleted = _clients.SingleOrDefault(x => x.Id == message.Value);

            if (clientDeleted is not null)
            {
                _clients.Remove(clientDeleted);
            }
        });
    }

    public ObservableCollection<Client> Clients
    {
        get => _clients;
        private set => SetValue(ref _clients, value);
    }

    public Client SelectedClient { get; set; } = new Client();

    public ICommand ClientSelectedCommand { get; set; }

    public ICommand AddClientCommand { get; set; }
}