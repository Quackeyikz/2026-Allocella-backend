using System.ComponentModel.DataAnnotations;

namespace AllocellaAPI.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]

    public string FullName { get; set; } = string.Empty;

    // Hashed password, for more security.
    // We DO NOT want to store original pass in database >:u ~ @Quackeyikz
    // WHy string empty? To prevent null erros, my IDE yells at me that it shouldnt be null when exiting constructor,
    // So I added default value of string.Empty (not null)
    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    public string Role { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // A user can have many bookings.
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
