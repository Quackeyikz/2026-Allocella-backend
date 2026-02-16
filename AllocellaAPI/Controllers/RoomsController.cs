using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AllocellaAPI.DTOs.Rooms;
using AllocellaAPI.Services.Rooms;

namespace AllocellaAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;
    private readonly ILogger<RoomsController> _logger;

    public RoomsController(IRoomService roomService, ILogger<RoomsController> logger)
    {
        _roomService = roomService;
        _logger = logger;
    }

    // POST /rooms - Create room (admin only)
    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> CreateRoom([FromBody] RoomCreateRequest request)
    {
        try
        {
            var result = await _roomService.CreateRoomAsync(request);
            _logger.LogInformation("Room {RoomId} created: {RoomNumber}", result.Id, result.RoomNumber);
            return CreatedAtAction(nameof(GetRoomById), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating room");
            return StatusCode(500, new { message = "An error occurred while creating the room" });
        }
    }

    // GET /rooms - Get all rooms (public)
    [HttpGet]
    public async Task<IActionResult> GetAllRooms([FromQuery] bool? isAvailable = null)
    {
        try
        {
            var rooms = await _roomService.GetAllRoomsAsync(isAvailable);
            return Ok(rooms);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving rooms");
            return StatusCode(500, new { message = "An error occurred while retrieving rooms" });
        }
    }

    // GET /rooms/search - Search rooms by name, number, building, floor
    // Examples:
    // /rooms/search?search=Classroom
    // /rooms/search?search=C-303
    // /rooms/search?building=Main
    // /rooms/search?search=05&building=Science&floor=1
    [HttpGet("search")]
    public async Task<IActionResult> SearchRooms(
        [FromQuery] string? search = null,
        [FromQuery] string? building = null,
        [FromQuery] int? floor = null)
    {
        try
        {
            var rooms = await _roomService.SearchRoomsAsync(search, building, floor);
            return Ok(rooms);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching rooms");
            return StatusCode(500, new { message = "An error occurred while searching rooms" });
        }
    }

    // GET /rooms/{id} - Get room by ID (public)
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoomById(int id)
    {
        try
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null)
                return NotFound(new { message = "Room not found" });

            return Ok(room);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving room {RoomId}", id);
            return StatusCode(500, new { message = "An error occurred while retrieving the room" });
        }
    }

    // PUT /rooms/{id} - Update room (admin only)
    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateRoom(int id, [FromBody] RoomUpdateRequest request)
    {
        try
        {
            var result = await _roomService.UpdateRoomAsync(id, request);
            _logger.LogInformation("Room {RoomId} updated", id);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating room {RoomId}", id);
            return StatusCode(500, new { message = "An error occurred while updating the room" });
        }
    }

    // DELETE /rooms/{id} - Delete room (admin only)
    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteRoom(int id)
    {
        try
        {
            var result = await _roomService.DeleteRoomAsync(id);
            if (!result)
                return NotFound(new { message = "Room not found" });

            _logger.LogInformation("Room {RoomId} deleted", id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting room {RoomId}", id);
            return StatusCode(500, new { message = "An error occurred while deleting the room" });
        }
    }
}