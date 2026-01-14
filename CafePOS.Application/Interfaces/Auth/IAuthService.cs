using CafePOS.Application.DTOs.Auth;

namespace CafePOS.Application.Interfaces.Auth;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(LoginRequest loginRequest);
    Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest tokenRequest);
}