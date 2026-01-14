using CafePOS.Domain.Entities;

namespace CafePOS.Application.Interfaces.Persistence;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken token);
    Task<RefreshToken?> GetValidAsync(string token);
    Task RevokeAsync(RefreshToken token);
}