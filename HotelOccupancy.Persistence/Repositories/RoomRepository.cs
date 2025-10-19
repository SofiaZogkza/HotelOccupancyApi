using HotelOccupancy.Application;
using HotelOccupancy.Application.Common;
using HotelOccupancy.Domain.Models;
using HotelOccupancy.Domain.Models.Errors;
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
    
    public async Task<Result<List<Room>>> GetAllAsync(DateTime? date = null)
    {
        var entities = await _context.Rooms
            .Include(r => r.AssignedTravellers)
            .ThenInclude(t => t.TravelGroup)
            .ToListAsync();

        var rooms = entities.Select(RoomEntityMapper.ToDomain).ToList();
        return Result<List<Room>>.Success(rooms);
    }

    public async Task<Result<Room>> GetByIdAsync(Guid id)
    {
        var entity = await _context.Rooms
            .Include(r => r.AssignedTravellers)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (entity == null)
            return Result<Room>.Failure(ErrorCodes.NotFound, ErrorMessages.NotFound);

        return Result<Room>.Success(RoomEntityMapper.ToDomain(entity));
    }

    public async Task<Result<Room>> GetByCodeAsync(string code)
    {
        var entity = await _context.Rooms
            .Include(r => r.AssignedTravellers)
            .FirstOrDefaultAsync(r => r.Code == code);

        if (entity == null)
        {
            return Result<Room>.Failure(ErrorCodes.NotFound, ErrorMessages.NotFound);
        }

        return Result<Room>.Success(RoomEntityMapper.ToDomain(entity));
    }

    public async Task<Result<Room>> UpdateAsync(Room room)
    {
        var entity = await _context.Rooms
            .Include(r => r.AssignedTravellers)
            .FirstOrDefaultAsync(r => r.Id == room.Id);

        if (entity == null)
            return Result<Room>.Failure(ErrorCodes.NotFound, ErrorMessages.NotFound);

        // Map the updated values
        entity.Code = room.Code;
        entity.BedCount = room.BedCount;
        entity.AssignedTravellers = room.AssignedTravellers
            .Select(t => TravellerEntityMapper.ToEntity(t))
            .ToList();

        _context.Rooms.Update(entity);
        await _context.SaveChangesAsync();

        return Result<Room>.Success(RoomEntityMapper.ToDomain(entity));
    }
}