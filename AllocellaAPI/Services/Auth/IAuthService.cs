using AllocellaAPI.DTOs.Auth;
using AllocellaAPI.Models;

namespace AllocellaAPI.Services.Auth;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterAccountRequest request);
    Task<AuthResponse?> LoginAsync(LoginRequest request);
    string GenerateJwtToken(User user);
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
}