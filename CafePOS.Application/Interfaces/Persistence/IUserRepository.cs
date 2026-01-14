using CafePOS.Domain.Entities;

namespace CafePOS.Application.Interfaces.Persistence;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
}