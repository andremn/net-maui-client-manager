using Clients.Model;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Clients.Messaging;

internal class SelectedClientChangedMessage(Client value) : ValueChangedMessage<Client>(value)
{
}
