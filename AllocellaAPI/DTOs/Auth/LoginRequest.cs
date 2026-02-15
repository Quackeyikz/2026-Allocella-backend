using System.ComponentModel.DataAnnotations;

namespace AllocellaAPI.DTOs.Auth;

public class LoginRequest
{
    [Required(ErrorMessage = "Password is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
}