namespace CafePOS.Application.Interfaces.Security;

public interface IPasswordHasher
{
    string Hash(string password);
    bool verify(string password, string hash);
}