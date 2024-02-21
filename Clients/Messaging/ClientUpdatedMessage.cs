using Clients.Model;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Clients.Messaging;

internal class ClientUpdatedMessage(Client value) : ValueChangedMessage<Client>(value)
{
}
