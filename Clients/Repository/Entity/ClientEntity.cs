using SQLite;
using System.ComponentModel.DataAnnotations;

namespace Clients.Repository.Entity;

internal class ClientEntity
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public ushort Age { get; set; }

    [Required]
    public string AddressStreet { get; set; } = string.Empty;

    [Required]
    public ushort AddressNumber { get; set; }

    [Required]
    public string AddressCity { get; set; } = string.Empty;

    [Required]
    public string AddressZipCode { get; set; } = string.Empty;
}
