namespace _2026_Allocella_backend.Models;

public class Room
{
    public int Id { get; set; }
    public string RoomNumber { get; set; } = "";
    public string RoomName { get; set; } = "Classroom";
    public int Capacity { get; set; }
    public bool IsAvailable { get; set; }
}