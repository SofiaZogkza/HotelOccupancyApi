using HotelOccupancy.Application;
using HotelOccupancy.Domain.Models;
using HotelOccupancy.Persistence.Data;
using HotelOccupancy.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;

namespace HotelOccupancy.Persistence.Repositories;

public class TravelGroupRepository : ITravelGroupRepository
{
    private readonly HotelOccupancyDbContext _context;

    public TravelGroupRepository(HotelOccupancyDbContext context)
    {
        _context = context;
    }

    public async Task<List<TravelGroup>> GetAllAsync()
    {
        var entities = await _context.TravelGroups
            .Include(g => g.Travellers)
            .ToListAsync();

        return entities.Select(TravelGroupEntityMapper.ToDomain).ToList();
    }

    public async Task<TravelGroup?> GetByIdAsync(Guid id)
    {
        var entity = await _context.TravelGroups
            .Include(g => g.Travellers)
            .FirstOrDefaultAsync(g => g.Id == id);

        return entity == null ? null : TravelGroupEntityMapper.ToDomain(entity);
    }

    public async Task<TravelGroup?> GetByGroupIdAsync(string groupId)
    {
        var entity = await _context.TravelGroups
            .Include(g => g.Travellers)
            .FirstOrDefaultAsync(g => g.GroupId == groupId);

        return entity == null ? null : TravelGroupEntityMapper.ToDomain(entity);
    }

    public async Task AddAsync(TravelGroup travelGroup)
    {
        var entity = TravelGroupEntityMapper.ToEntity(travelGroup);
        await _context.TravelGroups.AddAsync(entity);
    }
}