using Clients.Model;
using Clients.Repository.Entity;

namespace Clients.Extensions;

internal static class ClientExtensions
{
    public static ClientEntity ToEntity(this Client client) =>
        new()
        {
            Id = client.Id,
            Name = client.Name,
            LastName = client.LastName,
            Age = client.Age,
            AddressCity = client.Address?.City ?? string.Empty,
            AddressNumber = client.Address?.Number ?? 0,
            AddressStreet = client.Address?.Street ?? string.Empty,
            AddressZipCode = client.Address?.ZipCode ?? string.Empty
        };

    public static Client ToModel(this ClientEntity entity) =>
        new()
        {
            Id = entity.Id,
            Name = entity.Name,
            LastName = entity.LastName,
            Age = entity.Age,
            Address = new()
            {
                City = entity.AddressCity,
                Number = entity.AddressNumber,
                Street = entity.AddressStreet,
                ZipCode = entity.AddressZipCode
            }
        };
}
