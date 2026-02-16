using System.ComponentModel.DataAnnotations;

namespace AllocellaAPI.DTOs.Rooms;

public class RoomCreateRequest
{
    [MaxLength(255)]
    public string RoomName { get; set; } = "Classroom";

    [Required]
    [MaxLength(255)]
    public string RoomNumber { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string Building { get; set; } = string.Empty;

    [Required]
    public int Floor { get; set; }

    [Required]
    public int Capacity { get; set; }
}