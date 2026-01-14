namespace CafePOS.Application.DTOs.Auth;

public record AuthResponse (string AccessToken, string RefreshToken);