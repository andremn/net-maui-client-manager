using Clients.Model;

namespace Clients.Repository;

public interface IClientRepository
{
    Task<IList<Client>> GetAllAsync();

    Task AddOrUpdateAsync(Client client);

    Task DeleteByIdAsync(int id);
}
