using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HotelOccupancy.Persistence.Data;

public class HotelOccupancyDbContextFactory : IDesignTimeDbContextFactory<HotelOccupancyDbContext>
{
    public HotelOccupancyDbContext CreateDbContext(string[] args)
    {
        var envFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, ".env");
        Env.Load(envFilePath);
        var optionsBuilder = new DbContextOptionsBuilder<HotelOccupancyDbContext>();
        
        // Fixed connection string here for Design-time.
        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ??
                               $"Host={Environment.GetEnvironmentVariable("POSTGRES_HOST")};" +
                               $"Port={Environment.GetEnvironmentVariable("POSTGRES_PORT")};" +
                               $"Database={Environment.GetEnvironmentVariable("POSTGRES_DB")};" +
                               $"Username={Environment.GetEnvironmentVariable("POSTGRES_USER")};" +
                               $"Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")}";
        
        optionsBuilder.UseNpgsql(connectionString);

        return new HotelOccupancyDbContext(optionsBuilder.Options);
    }
}