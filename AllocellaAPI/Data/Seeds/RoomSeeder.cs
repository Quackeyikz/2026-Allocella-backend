using AllocellaAPI.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace AllocellaAPI.Data.Seeds;

public static class RoomSeeder
{
    public static void SeedRooms(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>().HasData(
            new Room
            {
                Id = 1,
                RoomName = "M-Sect Laboratorium",
                RoomNumber = "05.13",
                Building = "PS",
                Floor = 5,
                Capacity = 16,
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            },

            new Room
            {
                Id = 2,
                RoomName = "Agile Laboratorium",
                RoomNumber = "C-203",
                Building = "D4",
                Floor = 2,
                Capacity = 32,
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            },

            new Room
            {
                Id = 3,
                RoomName = "Storage",
                RoomNumber = "A-101",
                Building = "D4",
                Floor = 1,
                Capacity = 16,
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            },

            new Room
            {
                Id = 4,
                RoomName = "Network Laboratorium",
                RoomNumber = "C-305",
                Building = "D4",
                Floor = 3,
                Capacity = 32,
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            },

            new Room
            {
                Id = 5,
                RoomName = "Operating System Laboratorium",
                RoomNumber = "C-305",
                Building = "D4",
                Floor = 3,
                Capacity = 32,
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            },

            new Room
            {
                Id = 6,
                RoomName = "Computer Laboratorium",
                RoomNumber = "C-205",
                Building = "D4",
                Floor = 2,
                Capacity = 32,
                IsAvailable = false,
                CreatedAt = DateTime.UtcNow
            },

            new Room
            {
                Id = 7,
                RoomName = "Classroom",
                RoomNumber = "B-201",
                Building = "D4",
                Floor = 2,
                Capacity = 64,
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            },

            new Room
            {
                Id = 8,
                RoomName = "Big Classroom",
                RoomNumber = "10.08",
                Building = "SAW",
                Floor = 10,
                Capacity = 128,
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            },

            new Room
            {
                Id = 9,
                RoomName = "Small Auditorium",
                RoomNumber = "HH-101",
                Building = "D3",
                Floor = 1,
                Capacity = 160,
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            },

            new Room
            {
                Id = 10,
                RoomName = "Big Auditorium",
                RoomNumber = "06.06",
                Building = "PS",
                Floor = 6,
                Capacity = 204,
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            }
        );
    }
}