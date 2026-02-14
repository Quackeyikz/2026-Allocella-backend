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

        // Using custom email trail cuz why not (does not exist, don't try this at home)
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Email = "admin@allocella.com",
                FullName = "System Administrator",
                PasswordHash = adminPasswordHash,
                Role = "admin",
                CreatedAt = DateTime.UtcNow
            },

            new User
            {
                Id = 2,
                Email = "eriktriayudaw@allocella.com",
                FullName = "Erik Triayuda Wijaya",
                PasswordHash = studentPasswordHash,
                Role = "student",
                CreatedAt = DateTime.UtcNow
            },

            new User
            {
                Id = 3,
                Email = "lepine@allocella.com",
                FullName = "Lepine Pauline",
                PasswordHash = studentPasswordHash,
                Role = "student",
                CreatedAt = DateTime.UtcNow
            },

            new User
            {
                Id = 4,
                Email = "ellen@allocella.com",
                FullName = "Dr. Ellen Lionhart S.Tr.Kom",
                PasswordHash = lecturerPasswordHash,
                Role = "lecturer",
                CreatedAt = DateTime.UtcNow
            },

            new User
            {
                Id = 5,
                Email = "robert@allocella.com",
                FullName = "Dr. Robert Oppenheimer Ka.Boom",
                PasswordHash = lecturerPasswordHash,
                Role = "lecturer",
                CreatedAt = DateTime.UtcNow
            }
        );
    }
}