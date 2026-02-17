using Microsoft.EntityFrameworkCore;
using AllocellaAPI.Models;

namespace AllocellaAPI.Data;

// THIS IS IT! This is what gives "context" between the back-end with the db
// This file determines the foreign key relation and indexing for performance.
public class AllocellaDbContext : DbContext
{
    public AllocellaDbContext(DbContextOptions<AllocellaDbContext> options) : base(options)
    {
        
    }

    // Each DbSet<T> represents a table (property name becomes plural btw)
    public DbSet<User> Users {get;set;} = null!;
    public DbSet<Room> Rooms {get;set;} = null!;
    public DbSet<Booking> Bookings {get;set;} = null!;
    public DbSet<BookingHistory> BookingHistories {get;set;} = null!;

    // Configures the db schema, it is called automatically when EF Core creates / updates the db, hehe :3
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Email).IsUnique();

            // A to B relation, B to A relation in that order. See models to code these.
            entity.HasMany(u => u.Bookings).WithOne(b => b.User).HasForeignKey(b => b.UserId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasMany(r => r.Bookings).WithOne(b => b.Room).HasForeignKey(b => b.RoomId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasIndex(b => b.Status);

            entity.HasIndex(b => new { b.StartTime, b.EndTime });

            entity.HasMany(b => b.History).WithOne(h => h.Booking).HasForeignKey(h => h.BookingId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<BookingHistory>(entity =>
        {
            entity.HasOne(h => h.ChangedBy).WithMany().HasForeignKey(h => h.ChangedByUserId).OnDelete(DeleteBehavior.SetNull);
        });

        // Data Seeding
        Seeds.UserSeeder.SeedUsers(modelBuilder);
        Seeds.RoomSeeder.SeedRooms(modelBuilder);
        Seeds.BookingSeeder.SeedBookings(modelBuilder);
    }
}