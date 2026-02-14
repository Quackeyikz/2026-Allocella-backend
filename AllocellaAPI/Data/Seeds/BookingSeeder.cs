using AllocellaAPI.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace AllocellaAPI.Data.Seeds;

public static class BookingSeeder
{
    public static void SeedBookings(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>().HasData(
            new Booking
            {
                Id = 1,
                RoomId = 3,
                UserId = 5,
                StartTime = DateTime.UtcNow.Date.AddDays(0).AddHours(7),
                EndTime = DateTime.UtcNow.Date.AddDays(10).AddHours(3),
                Purpose = "Cook",
                Status = "rejected",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            
            new Booking
            {
                Id = 2,
                RoomId = 6,
                UserId = 3,
                StartTime = DateTime.UtcNow.Date.AddDays(1).AddHours(5),
                EndTime = DateTime.UtcNow.Date.AddDays(3).AddHours(8),
                Purpose = "Communal brief with a movie shooting crew",
                Status = "approved",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            new Booking
            {
                Id = 3,
                RoomId = 10,
                UserId = 4,
                StartTime = DateTime.UtcNow.Date.AddDays(0).AddHours(8),
                EndTime = DateTime.UtcNow.Date.AddDays(15).AddHours(0),
                Purpose = "To declare the announcement that The Holy Grail war are now publicly known",
                Status = "pending",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            new Booking
            {
                Id = 4,
                RoomId = 1,
                UserId = 2,
                StartTime = DateTime.UtcNow.Date.AddDays(-2).AddHours(8),
                EndTime = DateTime.UtcNow.Date.AddDays(-1).AddHours(0),
                Purpose = "Community weekly meeting",
                Status = "completed",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );
    }
}