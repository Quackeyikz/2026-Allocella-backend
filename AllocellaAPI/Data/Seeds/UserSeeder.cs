using AllocellaAPI.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace AllocellaAPI.Data.Seeds;

public static class UserSeeder
{
    public static void SeedUsers(ModelBuilder modelBuilder)
    {
        var studentPasswordHash = BCrypt.Net.BCrypt.HashPassword("Student#123");
        var lecturerPasswordHash = BCrypt.Net.BCrypt.HashPassword("Lecturer#123");
        var adminPasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin#123");

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Email = "admin@allocella.com",
                PasswordHash = adminPasswordHash,
                FullName = "System Administrator",
                Role = "admin",
                CreatedAt = DateTime.UtcNow
            },

            new User
            {
                
            },

            new User
            {
                
            }
        );
    }
}