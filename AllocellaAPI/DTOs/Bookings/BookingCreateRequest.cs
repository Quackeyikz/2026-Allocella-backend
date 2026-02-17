using System.ComponentModel.DataAnnotations;

namespace AllocellaAPI.DTOs.Bookings;

public class BookingCreateRequest
{
    [Required]
    public int RoomId { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }
    
    [Required]
    [MaxLength(500)]
    public string Purpose { get; set; } = string.Empty;
}