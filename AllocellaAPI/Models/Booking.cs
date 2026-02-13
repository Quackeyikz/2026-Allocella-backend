using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllocellaAPI.Models;

public class Booking
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int RoomId { get; set; }

    // Foreign key to RoomId above ^
    [ForeignKey("RoomId")]
    public Room Room { get; set; } = null!;

    [Required]
    public int UserId { get; set; }

    // Refactor: Changed from student to user to avoid inscalability
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [Required]
    [MaxLength(500)]
    public string Purpose { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = "pending";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<BookingHistory> History { get; set; } = new List<BookingHistory>();
}