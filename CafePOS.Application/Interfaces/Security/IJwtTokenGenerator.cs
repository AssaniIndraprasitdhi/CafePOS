using CafePOS.Domain.Entities;

namespace CafePOS.Application.Interfaces.Security;

public interface IJwtTokenGenerator
{
    string GenerateAccessToken(User user);
    RefreshToken GenerateRefreshToken(Guid userId);
}