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
    
        modelBuilder.Entity<RoomEntity>()
            .HasKey(r => r.Id);

        modelBuilder.Entity<RoomEntity>()
            .HasMany(r => r.AssignedTravellers)
            .WithOne(t => t.Room)
            .HasForeignKey(t => t.RoomId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<TravellerEntity>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<TravellerEntity>()
            .Property(t => t.RoomId)
            .IsRequired(false);

        // Traveller -> TravelGroup relationship
        modelBuilder.Entity<TravellerEntity>()
            .HasOne(t => t.TravelGroup)             // navigation property
            .WithMany(g => g.Travellers)            // navigation property in TravelGroupEntity
            .HasForeignKey(t => t.TravelGroupId)    // FK in TravellerEntity
            .HasPrincipalKey(g => g.GroupId)        // PK-like property in TravelGroupEntity
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TravelGroupEntity>()
            .HasKey(g => g.Id);

        modelBuilder.Entity<RoomEntity>()
            .HasIndex(r => r.Code)
            .IsUnique();

        modelBuilder.Entity<TravelGroupEntity>()
            .HasIndex(g => g.GroupId)
            .IsUnique();
    }
}