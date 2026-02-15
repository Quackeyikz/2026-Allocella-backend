using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using AllocellaAPI.Data;
using AllocellaAPI.DTOs.Auth;
using AllocellaAPI.Models;
using BCrypt.Net;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

/*
    Quackeyikz here, this is "THE Auth Service"

    This file solely handles registration and login account, with the help of DTOs validations.
    Refer to DTOs/Auth/*.
    This file implements the interface of IAuthservice, i'm lazy to separate the folder.

    Q: "How can I change on what information the FE will get?"
    A: Go to `return new AuthResponse` section and add it there. Don't add password information.
*/

namespace AllocellaAPI.Services.Auth;

public class AuthService : IAuthService
{
    private readonly AllocellaDbContext _context;

    // To apply context & app config
    public AuthService(AllocellaDbContext context)
    {
        _context = context;
    }

    // REGISTRATION: Returns the AuthResponse DTO (Token information)
    public async Task<AuthResponse> RegisterAsync(RegisterAccountRequest request)
    {
        // Step 1: First, make sure email is unique. Check if email existed (well duh!)
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

        // If it does exist, throw an exception
        if (existingUser != null)
        {
            throw new InvalidOperationException("Email already registered");
        }

        // Setp 2: Hash the password by using AuthService (this file) method.
        var passwordHash = HashPassword(request.Password);

        // Step 3: Make a new user, will be assigned to db
        var user = new User
        {
            Email = request.Email,
            PasswordHash = passwordHash,
            FullName = request.FullName,
            Role = request.Role,
            CreatedAt = DateTime.UtcNow
        };

        // Step 4: Add to database
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Step 5: Generate JWT Token - JWT Time baby!
        var token = GenerateJwtToken(user);

        // Step 6: Return the response
        return new AuthResponse
        {
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddHours(6),
            User = new UserInfo
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role
            },
            Message = "Registration successful!"
        };
    }

    // LOGIN
    public async Task<AuthResponse?> LoginAsync(LoginRequest request)
    {
        // Step 1: Find the email
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

        // Step 2: The opposite of register, if it exist then GET OUT!
        if (user == null)
        {
            return null;
        }

        // Step 3: Verify password
        bool isPasswordValid = VerifyPassword(request.Password, user.PasswordHash);

        if (!isPasswordValid)
        {
            return null;
        }

        // Step 4: JWT Time baby!
        var token = GenerateJwtToken(user);

        // Step 5: Return response
        return new AuthResponse 
        {
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddHours(6),
            User = new UserInfo
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role
            },
            Message = "Login successful!"
        };
    }
    public string GenerateJwtToken(User user)
    {
        // Piece of information about the user: stored in the payload. Remember the [Header - Payload - Signature]
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Changed: Make it to read the .env instead of appsettings.json
        var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? throw new InvalidOperationException("JWT_SECRET not configured");
        var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "AllocellaAPI";
        var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "AllocellaClient";
        var jwtExpiryHours = int.Parse(Environment.GetEnvironmentVariable("JWT_EXPIRY_HOURS") ?? "24");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Token descriptor
        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(jwtExpiryHours),
            signingCredentials: credentials
        );

        // Stringify the token
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // "Why do we need a whole method? well cuz its used on multiple times, too long of a line
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
    }

    // Comparing passwords, basically
    // Source: https://jasonwatmore.com/post/2022/01/16/net-6-hash-and-verify-passwords-with-bcrypt
    public bool VerifyPassword(string password, string passwordHash)
    {
        try
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
        catch
        {
            return false;
        }
    }
}