using Microsoft.EntityFrameworkCore;
using AllocellaAPI.Data;
using AllocellaAPI.DTOs.Rooms;
using AllocellaAPI.Models;

namespace AllocellaAPI.Services.Rooms;

public class RoomService : IRoomService
{
    private readonly AllocellaDbContext _context;
    private readonly ILogger<RoomService> _logger;

    public RoomService(AllocellaDbContext context, ILogger<RoomService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<RoomResponse> CreateRoomAsync(RoomCreateRequest request)
    {
        // RoomNumber must be unique
        var exists = await _context.Rooms
            .AnyAsync(r => r.RoomNumber == request.RoomNumber);

        if (exists)
            throw new InvalidOperationException($"Room number '{request.RoomNumber}' already exists");

        var room = new Room
        {
            RoomName = request.RoomName,
            RoomNumber = request.RoomNumber,
            Building = request.Building,
            Floor = request.Floor,
            Capacity = request.Capacity,
            IsAvailable = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();

        return MapToResponse(room);
    }

    public async Task<List<RoomResponse>> GetAllRoomsAsync(bool? isAvailable = null)
    {
        var query = _context.Rooms.AsQueryable();

        if (isAvailable.HasValue)
            query = query.Where(r => r.IsAvailable == isAvailable.Value);

        var rooms = await query
            .OrderBy(r => r.Building)
            .ThenBy(r => r.Floor)
            .ThenBy(r => r.RoomNumber)
            .ToListAsync();

        return rooms.Select(MapToResponse).ToList();
    }

    public async Task<RoomResponse?> GetRoomByIdAsync(int id)
    {
        var room = await _context.Rooms.FindAsync(id);
        return room == null ? null : MapToResponse(room);
    }

    public async Task<RoomResponse> UpdateRoomAsync(int id, RoomUpdateRequest request)
    {
        var room = await _context.Rooms.FindAsync(id);
        if (room == null)
            throw new InvalidOperationException("Room not found");

        // Check if new RoomNumber is unique (excluding current room)
        var exists = await _context.Rooms
            .AnyAsync(r => r.Id != id && r.RoomNumber == request.RoomNumber);

        if (exists)
            throw new InvalidOperationException($"Room number '{request.RoomNumber}' already exists");

        room.RoomName = request.RoomName;
        room.RoomNumber = request.RoomNumber;
        room.Building = request.Building;
        room.Floor = request.Floor;
        room.Capacity = request.Capacity;
        room.IsAvailable = request.IsAvailable;

        await _context.SaveChangesAsync();

        return MapToResponse(room);
    }

    public async Task<bool> DeleteRoomAsync(int id)
    {
        var room = await _context.Rooms.FindAsync(id);
        if (room == null)
            return false;

        // Check if room has bookings
        var hasBookings = await _context.Bookings.AnyAsync(b => b.RoomId == id);
        if (hasBookings)
            throw new InvalidOperationException("Cannot delete room with existing bookings");

        _context.Rooms.Remove(room);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<RoomResponse>> SearchRoomsAsync(string? search = null, string? building = null, int? floor = null)
    {
        var query = _context.Rooms.Where(r => r.IsAvailable).AsQueryable();

        // Search by RoomName OR RoomNumber
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(r =>
                r.RoomName.Contains(search) ||
                r.RoomNumber.Contains(search));
        }

        // Filter by building
        if (!string.IsNullOrEmpty(building))
            query = query.Where(r => r.Building.Contains(building));

        // Filter by floor
        if (floor.HasValue)
            query = query.Where(r => r.Floor == floor.Value);

        var rooms = await query
            .OrderBy(r => r.Building)
            .ThenBy(r => r.Floor)
            .ThenBy(r => r.RoomNumber)
            .ToListAsync();

        return rooms.Select(MapToResponse).ToList();
    }

    private static RoomResponse MapToResponse(Room room)
    {
        return new RoomResponse
        {
            Id = room.Id,
            RoomName = room.RoomName,
            RoomNumber = room.RoomNumber,
            Building = room.Building,
            Floor = room.Floor,
            Capacity = room.Capacity,
            IsAvailable = room.IsAvailable,
            CreatedAt = room.CreatedAt
        };
    }
}