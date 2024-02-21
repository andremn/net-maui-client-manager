using Clients.Model;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Clients.Messaging;

internal class ClientAddedMessage(Client value) : ValueChangedMessage<Client>(value)
{
}
