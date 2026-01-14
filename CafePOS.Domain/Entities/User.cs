namespace CafePOS.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    
    public List<UserRole> UserRole { get; set; } = new();
}