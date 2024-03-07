using Clients.Messaging;
using Clients.Model;
using Clients.Repository;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;

namespace Clients.ViewModel;

public class EditClientViewModel : BaseViewModel
{
    private int _clientId;
    private EditClientPageState _state = new();

    public EditClientViewModel(IClientRepository clientRepository)
    {
        WeakReferenceMessenger.Default.Register<SelectedClientChangedMessage>(this, (_, message) =>
        {
            _clientId = message.Value.Id;
            State = EditClientPageState.FromClient(message.Value);
        });

        SaveCommand = new Command(async () =>
        {
            var client = State.ToClient(_clientId);

            await clientRepository.AddOrUpdateAsync(client);

            WeakReferenceMessenger.Default.Send(new ClientUpdatedMessage(client));

            NotifyNavigateBackRequested();
        });

        DeleteConfirmationRequestCommand = new Command(() =>
        {
            DeleteDialogRequest?.Invoke(this, EventArgs.Empty);
        });

        DeleteCommand = new Command(async  () =>
        {
            await clientRepository.DeleteByIdAsync(_clientId);

            WeakReferenceMessenger.Default.Send(new ClientDeletedMessage(_clientId));

            NotifyNavigateBackRequested();
        });
    }

    public event EventHandler? DeleteDialogRequest;

    public EditClientPageState State
    {
        get => _state;
        set => SetValue(ref _state, value);
    }

    public ICommand SaveCommand { get; set; }

    public ICommand DeleteCommand { get; set; }

    public ICommand DeleteConfirmationRequestCommand { get; set; }

    public record EditClientPageState(string Name = "", string LastName = "", string Age = "", string Street = "", string Number = "", string City = "", string ZipCode = "")
    {
        public Client ToClient(int id) =>
            new(
                Id: id,
                Name: Name,
                LastName: LastName,
                Age: ushort.Parse(Age),
                Address: new(
                    Street: Street,
                    Number: ushort.Parse(Number),
                    City: City,
                    ZipCode: ZipCode)
                );

        public static EditClientPageState FromClient(Client client) =>
            new(
                client.Name,
                client.LastName,
                client.Age.ToString(),
                client.Address?.Street ?? string.Empty,
                client.Address?.Number.ToString() ?? string.Empty,
                client.Address?.City ?? string.Empty,
                client.Address?.ZipCode ?? string.Empty
            );
    }
}
