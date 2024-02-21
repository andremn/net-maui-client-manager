using Clients.Model;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Clients.Messaging;

internal class ClientDeletedMessage(int id) : ValueChangedMessage<int>(id)
{
}
