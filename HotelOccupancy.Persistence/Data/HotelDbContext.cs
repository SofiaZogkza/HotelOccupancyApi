using HotelOccupancy.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelOccupancy.Persistence.Data;

public class HotelOccupancyDbContext : DbContext
{
    public HotelOccupancyDbContext(DbContextOptions<HotelOccupancyDbContext> options)
        : base(options)
    {
    }

    // DbSets
    public DbSet<RoomEntity> Rooms { get; set; }
    public DbSet<TravellerEntity> Travellers { get; set; }
    public DbSet<TravelGroupEntity> TravelGroups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<RoomEntity>() // public class RoomEntityConfiguration : IEntityTypeConfiguration<RoomEntity>
            .HasKey(r => r.Id);

        modelBuilder.Entity<RoomEntity>()
            .HasMany(r => r.AssignedTravellers)
            .WithOne(t => t.Room)
            .HasForeignKey(t => t.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TravellerEntity>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<TravelGroupEntity>()
            .HasKey(g => g.Id);

        modelBuilder.Entity<TravelGroupEntity>()
            .HasMany(g => g.Travellers)
            .WithOne()
            .HasForeignKey(t => t.TravelGroupId)
            .HasPrincipalKey(g => g.GroupId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<RoomEntity>()
            .HasIndex(r => r.Code)
            .IsUnique();

        modelBuilder.Entity<TravelGroupEntity>()
            .HasIndex(g => g.GroupId)
            .IsUnique();
    }
}