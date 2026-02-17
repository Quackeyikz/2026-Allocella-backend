using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace AllocellaAPI.DTOs.Auth;

public class RegisterAccountRequest
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [MaxLength(255, ErrorMessage = "Email cannot exceed 255 characters")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*\d).+$", ErrorMessage = "Password must contain at least one number")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Name is required")]
    [MinLength(2, ErrorMessage = "Name must be at least 2 character")]
    [MaxLength(255, ErrorMessage = "Name cannot exceed 255 characters long")]
    public string FullName { get; set; } = string.Empty;

    // Change this, i don't think admin should be a visible option at FE
    [Required(ErrorMessage = "Role is required")]
    [RegularExpression("^(student|lecturer|admin)$", ErrorMessage = "Role must be either 'student' or 'lecturer'")]
    public string Role { get; set; } = string.Empty;

    // CreatedAt will be handled in the service, not here bud
}