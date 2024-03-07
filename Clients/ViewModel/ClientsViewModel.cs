using Clients.Messaging;
using Clients.Model;
using Clients.Repository;
using Clients.Views;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace Clients.ViewModel;

public class ClientsViewModel : BaseViewModel
{
    private readonly IClientRepository _repository;
    
    private ObservableCollection<Client> _clients = [];

    public ClientsViewModel(IServiceProvider serviceProvider)
    {
        _repository = serviceProvider.GetRequiredService<IClientRepository>();

        ClientSelectedCommand = new Command(() =>
        {
            var targetPage = serviceProvider.GetRequiredService<EditClientPage>();
            
            NotifyNavigateToPageRequested(targetPage);

            WeakReferenceMessenger.Default.Send(new SelectedClientChangedMessage(SelectedClient));
        });

        AddClientCommand = new Command(() =>
        {
            var targetPage = serviceProvider.GetRequiredService<AddClientPage>();
            
            NotifyNavigateToPageRequested(targetPage);
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

    public async Task LoadClientsAsync()
    {
        var clients = await _repository.GetAllAsync();

        foreach (var client in clients)
        {
            _clients.Add(client);
        }
    }
}