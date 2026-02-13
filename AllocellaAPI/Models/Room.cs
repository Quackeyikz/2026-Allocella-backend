using System.ComponentModel.DataAnnotations;

namespace AllocellaAPI.Models;

public class Room
{
    [Key]
    public int Id { get; set; }

    [MaxLength(255)]
    public string RoomName { get; set; } = "Classroom";

    [Required]
    public string RoomNumber { get; set; } = string.Empty;

    [Required]
    public string Building { get; set; } = string.Empty;

    [Required]
    public int Floor { get; set; }

    [Required]
    public int Capacity { get; set; }

    public bool IsAvailable {get;set;} = true;

    public DateTime CreatedAt {get;set;} = DateTime.UtcNow;

    // One room can have many bookings.
    public ICollection<Booking> Bookings {get;set;} = new List<Booking>();
}