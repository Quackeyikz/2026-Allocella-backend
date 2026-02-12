using _2026_Allocella_backend.Models;

namespace _2026_Allocella_backend.Services;

// In-memory data
public static class RoomService
{
    static List<Room> Rooms { get; }
    static int nextId = 3;
    static RoomService()
    {
        Rooms = new List<Room>
        {
            new Room { Id = 1, RoomNumber = "PS-05.13", RoomName = "M-SECT Lab", Capacity = 15, IsAvailable = false },
            new Room { Id = 2, RoomNumber = "C-203", RoomName = "Classroom", Capacity = 32, IsAvailable = true }
        };
    }

    public static List<Room> GetAll() => Rooms;

    public static Room? Get(int id) => Rooms.FirstOrDefault(p => p.Id == id);

    public static void Add(Room room)
    {
        room.Id = nextId++;
        Rooms.Add(room);
    }

    public static void Delete(int id)
    {
        var room = Get(id);
        if(room is null)
            return;

        Rooms.Remove(room);
    }

    public static void Update(Room room)
    {
        var index = Rooms.FindIndex(p => p.Id == room.Id);
        if(index == -1)
            return;

        Rooms[index] = room;
    }
}