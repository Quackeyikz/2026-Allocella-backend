using AllocellaAPI.DTOs.Rooms;

namespace AllocellaAPI.Services.Rooms;

public interface IRoomService
{
    Task<RoomResponse> CreateRoomAsync(RoomCreateRequest request);
    Task<List<RoomResponse>> GetAllRoomsAsync(bool? isAvailable = null);
    Task<RoomResponse?> GetRoomByIdAsync(int id);
    Task<RoomResponse> UpdateRoomAsync(int id, RoomUpdateRequest request);
    Task<bool> DeleteRoomAsync(int id);
    Task<List<RoomResponse>> SearchRoomsAsync(string? search = null, string? building = null, int? floor = null);
}