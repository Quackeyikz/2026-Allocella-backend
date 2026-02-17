using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllocellaAPI.Models;

public class BookingHistory
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int BookingId { get; set; }

    [ForeignKey("BookingId")]
    public Booking Booking { get; set; } = null!;

    [MaxLength(50)]
    public string? OldStatus { get; set; }

    [Required]
    [MaxLength(50)]
    public string NewStatus { get; set; } = string.Empty;

    public int? ChangedByUserId { get; set; }

    [ForeignKey("ChangedByUserId")]
    public User? ChangedBy { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    [MaxLength(500)]
    public string? Comment { get; set; }
}