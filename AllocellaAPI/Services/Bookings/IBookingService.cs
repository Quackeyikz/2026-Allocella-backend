using AllocellaAPI.DTOs.Bookings;

namespace AllocellaAPI.Services.Bookings
{
    public interface IBookingService
    {
        Task<BookingResponse> CreateBookingAsync(int userId, BookingCreateRequest request);
        Task<List<BookingResponse>> GetAllBookingsAsync(string? status = null, int? roomId = null);
        Task<List<BookingResponse>> GetUserBookingsAsync(int userId);
        Task<BookingResponse?> GetBookingByIdAsync(int id);
        Task<BookingResponse> UpdateBookingAsync(int id, BookingUpdateRequest request);
        Task<bool> DeleteBookingAsync(int id);
        Task<BookingResponse> ChangeStatusAsync(int id, string newStatus, int adminId);
    }
}