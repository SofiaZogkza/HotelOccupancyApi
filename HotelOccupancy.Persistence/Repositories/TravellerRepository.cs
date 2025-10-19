using HotelOccupancy.Application;
using HotelOccupancy.Domain.Models;
using HotelOccupancy.Persistence.Data;
using HotelOccupancy.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;

namespace HotelOccupancy.Persistence.Repositories;

public class TravellerRepository : ITravellerRepository
{
    private readonly HotelOccupancyDbContext _context;

    public TravellerRepository(HotelOccupancyDbContext context)
    {
        _context = context;
    }

    public async Task<List<Traveller>> GetAllAsync()
    {
        var entities = await _context.Travellers
            .Include(t => t.Room)
            .ToListAsync();

        return entities.Select(TravellerEntityMapper.ToDomain).ToList();
    }

    public async Task<Traveller?> GetByIdAsync(Guid id)
    {
        var entity = await _context.Travellers
            .Include(t => t.Room)
            .FirstOrDefaultAsync(t => t.Id == id);

        return entity == null ? null : TravellerEntityMapper.ToDomain(entity);
    }

    public async Task<List<Traveller>> GetByTravelGroupIdAsync(string travelGroupId)
    {
        var entities = await _context.Travellers
            .Where(t => t.TravelGroupId == travelGroupId)
            .Include(t => t.Room)
            .ToListAsync();

        return entities.Select(TravellerEntityMapper.ToDomain).ToList();
    }

    public async Task AddAsync(Traveller traveller)
    {
        var entity = TravellerEntityMapper.ToEntity(traveller);
        await _context.Travellers.AddAsync(entity);
    }
}