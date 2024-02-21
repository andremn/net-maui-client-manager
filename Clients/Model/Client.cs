namespace Clients.Model;

public record Client(int Id = 0, string Name = "", string LastName = "", ushort Age = 0, Address? Address = null);
