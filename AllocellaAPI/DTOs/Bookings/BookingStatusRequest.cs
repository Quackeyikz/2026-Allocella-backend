using System.ComponentModel.DataAnnotations;

namespace AllocellaAPI.DTOs.Bookings;

public class BookingStatusRequest
{
    [Required]
    [RegularExpression("^(approved|rejected)$")]
    public string Status {get;set;} = string.Empty;
    
    [Required]
    [MaxLength(500)]
    public string? Comment {get;set;}
}