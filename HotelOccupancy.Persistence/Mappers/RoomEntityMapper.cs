using HotelOccupancy.Domain.Models;
using HotelOccupancy.Persistence.Entities;

namespace HotelOccupancy.Persistence.Mappers;

public static class RoomEntityMapper
{
    public static RoomEntity ToEntity(Room room)
    {
        return new RoomEntity
        {
            Id = room.Id,
            Code = room.Code,
            BedCount = room.BedCount,
            AssignedTravellers = room.AssignedTravellers?.Select(t => TravellerEntityMapper.ToEntity(t)).ToList() ?? new List<TravellerEntity>()
        };
    }

    public static Room ToDomain(RoomEntity entity)
    {
        return new Room
        {
            Id = entity.Id,
            Code = entity.Code,
            BedCount = entity.BedCount,
            AssignedTravellers = entity.AssignedTravellers?.Select(t => TravellerEntityMapper.ToDomain(t)).ToList() ?? new List<Traveller>()
        };
    }
}