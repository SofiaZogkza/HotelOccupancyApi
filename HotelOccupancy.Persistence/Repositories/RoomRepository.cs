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
            .AsNoTracking()
            .Include(r => r.AssignedTravellers)
            .ThenInclude(t => t.TravelGroup)
            .ToListAsync();

        var rooms = entities.Select(RoomEntityMapper.ToDomain).ToList();
        return Result<List<Room>>.Success(rooms);
    }

    public async Task<Result<Room>> GetByIdAsync(Guid id)
    {
        var entity = await _context.Rooms
            .AsNoTracking()
            .Include(r => r.AssignedTravellers)
            .ThenInclude(t => t.TravelGroup)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (entity == null)
            return Result<Room>.Failure(ErrorCodes.NotFound, ErrorMessages.NotFound);

        return Result<Room>.Success(RoomEntityMapper.ToDomain(entity));
    }

    public async Task<Result<Room>> GetByCodeAsync(string code)
    {
        var entity = await _context.Rooms
            .AsNoTracking()
            .Include(r => r.AssignedTravellers)
            .ThenInclude(t => t.TravelGroup)
            .FirstOrDefaultAsync(r => r.Code == code);

        if (entity == null)
        {
            return Result<Room>.Failure(ErrorCodes.NotFound, ErrorMessages.NotFound);
        }

        return Result<Room>.Success(RoomEntityMapper.ToDomain(entity));
    }
    
    public async Task<Result<List<Room>>> GetRoomsByTravelGroupAsync(string travelGroupId)
    {
        if (string.IsNullOrWhiteSpace(travelGroupId))
        {
            return Result<List<Room>>.Failure(ErrorCodes.InvalidRequest, "TravelGroupId cannot be empty.");
        }

        var entities = await _context.Rooms
            .Include(r => r.AssignedTravellers)
            .ThenInclude(t => t.TravelGroup)
            .Where(r => r.AssignedTravellers.Any(t => t.TravelGroup.GroupId == travelGroupId))
            .ToListAsync();
        
        // Filter travellers in each room to only those matching the travel group
        foreach (var room in entities)
        {
            room.AssignedTravellers = room.AssignedTravellers
                .Where(t => t.TravelGroup.GroupId == travelGroupId)
                .ToList();
        }

        var rooms = entities.Select(RoomEntityMapper.ToDomain).ToList();
        return Result<List<Room>>.Success(rooms);
    }


    public async Task<Result<Room>> UpdateAsync(Room room)
    {
        var entity = await _context.Rooms
            .Include(r => r.AssignedTravellers)
                .ThenInclude(t => t.TravelGroup) // for each traveller, also load their TravelGroup
            .FirstOrDefaultAsync(r => r.Id == room.Id); // filter the room with specific id

        if (entity == null)
            return Result<Room>.Failure(ErrorCodes.NotFound, ErrorMessages.NotFound);

        // Update basic fields
        entity.Code = room.Code;
        entity.BedCount = room.BedCount;

        // Clear and reassign travellers
        entity.AssignedTravellers.Clear();

        foreach (var traveller in room.AssignedTravellers)
        {
            // Attach each traveller entity to context to avoid duplicate inserts
            var travellerEntity = await _context.Travellers.FindAsync(traveller.Id);

            if (travellerEntity != null)
            {
                entity.AssignedTravellers.Add(travellerEntity);
            }
            else
            {
                // New traveller (shouldn’t happen in move scenario, but just in case)
                entity.AssignedTravellers.Add(TravellerEntityMapper.ToEntity(traveller));
            }
        }

        // Save tracked changes (EF knows about `entity` and the modified collection)
        await _context.SaveChangesAsync();

        return Result<Room>.Success(RoomEntityMapper.ToDomain(entity));
    }

}