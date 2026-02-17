using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AllocellaAPI.DTOs.Bookings;
using AllocellaAPI.Services.Bookings;

namespace AllocellaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize] // All endpoints require authentication
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly ILogger<BookingsController> _logger;

        public BookingsController(IBookingService bookingService, ILogger<BookingsController> logger)
        {
            _bookingService = bookingService;
            _logger = logger;
        }

        // POST /bookings - Create booking (student/lecturer)
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreateRequest request)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var result = await _bookingService.CreateBookingAsync(userId, request);
                
                _logger.LogInformation("User {UserId} created booking {BookingId}", userId, result.Id);
                
                return CreatedAtAction(nameof(GetBookingById), new { id = result.Id }, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating booking");
                return StatusCode(500, new { message = "An error occurred while creating the booking" });
            }
        }

        // GET /bookings - Get all bookings (admin) or user bookings (student/lecturer)
        [HttpGet]
        public async Task<IActionResult> GetBookings([FromQuery] string? status = null, [FromQuery] int? roomId = null)
        {
            try
            {
                var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                List<BookingResponse> bookings;

                if (userRole == "admin")
                {
                    // Admin sees all bookings with filters
                    bookings = await _bookingService.GetAllBookingsAsync(status, roomId);
                }
                else
                {
                    // Students/lecturers see only their bookings
                    bookings = await _bookingService.GetUserBookingsAsync(userId);
                    
                    // Apply filters
                    if (!string.IsNullOrEmpty(status))
                        bookings = bookings.Where(b => b.Status == status).ToList();
                    
                    if (roomId.HasValue)
                        bookings = bookings.Where(b => b.RoomId == roomId.Value).ToList();
                }

                return Ok(bookings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving bookings");
                return StatusCode(500, new { message = "An error occurred while retrieving bookings" });
            }
        }

        // GET /bookings/{id} - Get booking details
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            try
            {
                var booking = await _bookingService.GetBookingByIdAsync(id);
                
                if (booking == null)
                    return NotFound(new { message = "Booking not found" });

                var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                // Students/lecturers can only view their own bookings
                if (userRole != "admin" && booking.UserId != userId)
                    return Forbid();

                return Ok(booking);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving booking {BookingId}", id);
                return StatusCode(500, new { message = "An error occurred while retrieving the booking" });
            }
        }

        // PUT /bookings/{id} - Update booking (own bookings only, pending status)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] BookingUpdateRequest request)
        {
            try
            {
                var booking = await _bookingService.GetBookingByIdAsync(id);
                
                if (booking == null)
                    return NotFound(new { message = "Booking not found" });

                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                // Only owner can update
                if (booking.UserId != userId)
                    return Forbid();

                var result = await _bookingService.UpdateBookingAsync(id, request);
                
                _logger.LogInformation("User {UserId} updated booking {BookingId}", userId, id);
                
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating booking {BookingId}", id);
                return StatusCode(500, new { message = "An error occurred while updating the booking" });
            }
        }

        // DELETE /bookings/{id} - Delete booking (admin only)
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            try
            {
                var result = await _bookingService.DeleteBookingAsync(id);
                
                if (!result)
                    return NotFound(new { message = "Booking not found" });

                var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                _logger.LogInformation("Admin {AdminId} deleted booking {BookingId}", adminId, id);
                
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting booking {BookingId}", id);
                return StatusCode(500, new { message = "An error occurred while deleting the booking" });
            }
        }

        // PATCH /bookings/{id}/status - Change booking status (admin only)
        [HttpPatch("{id}/status")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ChangeStatus(int id, [FromBody] BookingStatusRequest request)
        {
            try
            {
                var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var result = await _bookingService.ChangeStatusAsync(id, request.Status, adminId);
                
                _logger.LogInformation("Admin {AdminId} changed booking {BookingId} status to {Status}", 
                    adminId, id, request.Status);
                
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing booking {BookingId} status", id);
                return StatusCode(500, new { message = "An error occurred while changing the booking status" });
            }
        }
    }
}