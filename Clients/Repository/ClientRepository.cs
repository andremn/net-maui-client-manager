using Clients.Extensions;
using Clients.Model;
using Clients.Repository.Entity;

namespace Clients.Repository;

internal class ClientRepository(ApplicationDbContext context) : IClientRepository
{
    public async Task AddOrUpdateAsync(Client client)
    {
        var entity = client.ToEntity();
        var connection = await context.GetClientDatabaseAsync();

        if (entity.Id == 0)
        {
            await connection.InsertAsync(entity);
        }
        else
        {
            await connection.UpdateAsync(entity);
        }
    }

    public async Task DeleteByIdAsync(int id)
    {
        var connection = await context.GetClientDatabaseAsync();

        await connection.DeleteAsync<ClientEntity>(primaryKey: id);
    }

    public async Task<IList<Client>> GetAllAsync()
    {
        var connection = await context.GetClientDatabaseAsync();
        var entities = await connection.Table<ClientEntity>().ToListAsync();

        return entities.Select(x => x.ToModel()).ToList();

    }
}
