using Clients.Messaging;
using Clients.Model;
using Clients.Repository;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;

namespace Clients.ViewModel;

public class AddClientViewModel : BaseViewModel
{
    private AddClientPageState _state = new();

    public AddClientViewModel(IClientRepository clientRepository)
    {
        CancelCommand = new Command(() =>
        {
            CloseWindowRequest?.Invoke(this, EventArgs.Empty);
        });

        SaveCommand = new Command(async () =>
        {
            var client = State.ToClient();

            await clientRepository.AddOrUpdateAsync(client);

            WeakReferenceMessenger.Default.Send(new ClientAddedMessage(client));

            CloseWindowRequest?.Invoke(this, EventArgs.Empty);
        });
    }

    public event EventHandler? CloseWindowRequest;

    public AddClientPageState State
    {
        get => _state;
        set => SetValue(ref _state, value);
    }

    public ICommand CancelCommand { get; set; }

    public ICommand SaveCommand { get; set; }

    public record AddClientPageState(string Name = "", string LastName = "", string Age = "", string Street = "", string Number = "", string City = "", string ZipCode = "")
    {
        public Client ToClient() =>
            new(
                Id: 0,
                Name: Name,
                LastName: LastName,
                Age: ushort.Parse(Age),
                Address: new(
                    Street: Street,
                    Number: ushort.Parse(Number),
                    City: City,
                    ZipCode: ZipCode)
                );
    }
}
