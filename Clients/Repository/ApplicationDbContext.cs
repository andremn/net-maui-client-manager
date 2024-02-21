using Clients.Repository.Entity;
using SQLite;

namespace Clients.Repository;

internal class ApplicationDbContext
{
    private SQLiteAsyncConnection? _connection;

    public async Task<SQLiteAsyncConnection> GetClientDatabaseAsync()
    {
        if (_connection is null)
        {
            _connection = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await _connection.CreateTableAsync<ClientEntity>();
        }

        return _connection;
    }
}
