using Microsoft.EntityFrameworkCore;
using AllocellaAPI.Data;
using AllocellaAPI.DTOs.Bookings;
using AllocellaAPI.Models;

namespace AllocellaAPI.Services.Bookings;

// I'm too lazy to make an interface. We only need 1 service anyway.
public class BookingService : IBookingService
{
    private readonly AllocellaDbContext _context;
    private readonly ILogger<BookingService> _logger;

    public BookingService(AllocellaDbContext context, ILogger<BookingService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<BookingResponse> CreateBookingAsync(int userId, BookingCreateRequest request)
    {
        // Validate room exists
        var room = await _context.Rooms.FindAsync(request.RoomId);
        if (room == null)
            throw new InvalidOperationException("Room not found");

        if (!room.IsAvailable)
            throw new InvalidOperationException("Room is not available for booking");

        // Validate time range
        if (request.StartTime >= request.EndTime)
            throw new InvalidOperationException("End time must be after start time");

        if (request.StartTime < DateTime.UtcNow)
            throw new InvalidOperationException("Cannot book in the past");

        // Check for conflicts
        var hasConflict = await _context.Bookings
            .AnyAsync(b =>
                b.RoomId == request.RoomId &&
                b.Status != "rejected" &&
                ((request.StartTime >= b.StartTime && request.StartTime < b.EndTime) ||
                 (request.EndTime > b.StartTime && request.EndTime <= b.EndTime) ||
                 (request.StartTime <= b.StartTime && request.EndTime >= b.EndTime)));

        if (hasConflict)
            throw new InvalidOperationException("Room is already booked for this time slot");

        // Create booking
        var booking = new Booking
        {
            RoomId = request.RoomId,
            UserId = userId,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Purpose = request.Purpose,
            Status = "pending",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        // Create history record
        var history = new BookingHistory
        {
            BookingId = booking.Id,
            OldStatus = null,
            NewStatus = "pending",
            ChangedByUserId = userId,
            ChangedAt = DateTime.UtcNow,
            Comment = "Booking created"
        };

        _context.BookingHistories.Add(history);
        await _context.SaveChangesAsync();

        return await GetBookingByIdAsync(booking.Id)
            ?? throw new InvalidOperationException("Failed to retrieve created booking");
    }

    public async Task<List<BookingResponse>> GetAllBookingsAsync(string? status = null, int? roomId = null)
    {
        var query = _context.Bookings
            .Include(b => b.Room)
            .Include(b => b.User)
            .AsQueryable();

        if (!string.IsNullOrEmpty(status))
            query = query.Where(b => b.Status == status);

        if (roomId.HasValue)
            query = query.Where(b => b.RoomId == roomId.Value);

        var bookings = await query
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();

        return bookings.Select(MapToResponse).ToList();
    }

    public async Task<List<BookingResponse>> GetUserBookingsAsync(int userId)
    {
        var bookings = await _context.Bookings
            .Include(b => b.Room)
            .Include(b => b.User)
            .Where(b => b.UserId == userId)
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();

        return bookings.Select(MapToResponse).ToList();
    }

    public async Task<BookingResponse?> GetBookingByIdAsync(int id)
    {
        var booking = await _context.Bookings
            .Include(b => b.Room)
            .Include(b => b.User)
            .FirstOrDefaultAsync(b => b.Id == id);

        return booking == null ? null : MapToResponse(booking);
    }

    public async Task<BookingResponse> UpdateBookingAsync(int id, BookingUpdateRequest request)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null)
            throw new InvalidOperationException("Booking not found");

        if (booking.Status != "pending")
            throw new InvalidOperationException("Can only update pending bookings");

        // Validate time range
        if (request.StartTime >= request.EndTime)
            throw new InvalidOperationException("End time must be after start time");

        if (request.StartTime < DateTime.UtcNow)
            throw new InvalidOperationException("Cannot book in the past");

        // Check conflicts (excluding this booking)
        var hasConflict = await _context.Bookings
            .AnyAsync(b =>
                b.Id != id &&
                b.RoomId == booking.RoomId &&
                b.Status != "rejected" &&
                ((request.StartTime >= b.StartTime && request.StartTime < b.EndTime) ||
                 (request.EndTime > b.StartTime && request.EndTime <= b.EndTime) ||
                 (request.StartTime <= b.StartTime && request.EndTime >= b.EndTime)));

        if (hasConflict)
            throw new InvalidOperationException("Room is already booked for this time slot");

        booking.StartTime = request.StartTime;
        booking.EndTime = request.EndTime;
        booking.Purpose = request.Purpose;
        booking.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return await GetBookingByIdAsync(id)
            ?? throw new InvalidOperationException("Failed to retrieve updated booking");
    }

    public async Task<bool> DeleteBookingAsync(int id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null)
            return false;

        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<BookingResponse> ChangeStatusAsync(int id, string newStatus, int adminId)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null)
            throw new InvalidOperationException("Booking not found");

        var oldStatus = booking.Status;
        booking.Status = newStatus;
        booking.UpdatedAt = DateTime.UtcNow;

        // Create history record
        var history = new BookingHistory
        {
            BookingId = booking.Id,
            OldStatus = oldStatus,
            NewStatus = newStatus,
            ChangedByUserId = adminId,
            ChangedAt = DateTime.UtcNow,
            Comment = $"Status changed from {oldStatus} to {newStatus}"
        };

        _context.BookingHistories.Add(history);
        await _context.SaveChangesAsync();

        return await GetBookingByIdAsync(id)
            ?? throw new InvalidOperationException("Failed to retrieve updated booking");
    }

    private static BookingResponse MapToResponse(Booking booking)
    {
        return new BookingResponse
        {
            Id = booking.Id,
            RoomId = booking.RoomId,
            RoomName = booking.Room.RoomName,
            UserId = booking.UserId,
            UserName = booking.User.FullName,
            UserEmail = booking.User.Email,
            StartTime = booking.StartTime,
            EndTime = booking.EndTime,
            Purpose = booking.Purpose,
            Status = booking.Status,
            CreatedAt = booking.CreatedAt,
            UpdatedAt = booking.UpdatedAt
        };
    }

}