using HotelOccupancy.Application;
using HotelOccupancy.Domain.Models;
using HotelOccupancy.Persistence.Data;
using HotelOccupancy.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;

namespace HotelOccupancy.Persistence.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly HotelOccupancyDbContext _context;
    
    public RoomRepository(HotelOccupancyDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Room>> GetAllAsync(DateTime? date = null)
    {
        var entities = await _context.Rooms
            .Include(r => r.AssignedTravellers)
            .ToListAsync();

        var roomDomainModel = RoomEntityMapper.ToDomain;
        var result = entities.Select(roomDomainModel).ToList();
        return result;
    }

    public async Task<Room?> GetByIdAsync(Guid id)
    {
        var entity = await _context.Rooms
            .Include(r => r.AssignedTravellers)
            .FirstOrDefaultAsync(r => r.Id == id);

        return entity is null ? null : RoomEntityMapper.ToDomain(entity);
    }

    public async Task AddAsync(Room room)
    {
        var entity = RoomEntityMapper.ToEntity(room);
        await _context.Rooms.AddAsync(entity);
    }
}