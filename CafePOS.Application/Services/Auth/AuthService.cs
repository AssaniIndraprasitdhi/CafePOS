using CafePOS.Application.DTOs.Auth;
using CafePOS.Application.Interfaces.Auth;
using CafePOS.Application.Interfaces.Persistence;
using CafePOS.Application.Interfaces.Security;
using CafePOS.Domain.Entities;

namespace CafePOS.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IUserRepository _user;
    private readonly IPasswordHasher _hasher;
    private readonly IJwtTokenGenerator _jwt;
    private readonly IRefreshTokenRepository _refreshToken;

    public AuthService(IUserRepository user, IPasswordHasher hasher, IJwtTokenGenerator jwt, IRefreshTokenRepository refreshToken)
    {
        _user = user;
        _hasher = hasher;
        _jwt = jwt;
        _refreshToken = refreshToken;
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest loginRequest)
    {
        var user = await _user.GetByUsernameAsync(loginRequest.Username)
            ?? throw new Exception("Invalid credential");

        if (!_hasher.verify(loginRequest.Password, user.Password))
            throw new Exception("Invalid credentials");
        
        var accessToken = _jwt.GenerateAccessToken(user);
        var refreshToken = _jwt.GenerateRefreshToken(user.Id);

        await _refreshToken.AddAsync(refreshToken);

        return new AuthResponse(accessToken, refreshToken.Token); 
    }

    public async Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest)
    {
        var token = await _refreshToken.GetValidAsync(refreshTokenRequest.RefreshToken)
            ?? throw new Exception("Invalid refresh token");
        
        await _refreshToken.RevokeAsync(token);

        var accessToken = _jwt.GenerateAccessToken(token.User);
        var newRefreshToken = _jwt.GenerateRefreshToken(token.UserId);

        await _refreshToken.AddAsync(token);

        return new AuthResponse(accessToken, newRefreshToken.Token);
    }

}