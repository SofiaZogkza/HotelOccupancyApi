using HotelOccupancy.Domain.Models;
using HotelOccupancy.Persistence.Entities;

namespace HotelOccupancy.Persistence.Mappers;

public static class TravelGroupEntityMapper
{
    public static TravelGroupEntity ToEntity(TravelGroup group)
    {
        return new TravelGroupEntity
        {
            Id = group.Id,
            GroupId = group.GroupId,
            ArrivalDate = group.ArrivalDate,
            Travellers = group.Travellers?.Select(TravellerEntityMapper.ToEntity).ToList() 
                         ?? new List<TravellerEntity>()
        };
    }

    public static TravelGroup ToDomain(TravelGroupEntity entity)
    {
        return new TravelGroup
        {
            Id = entity.Id,
            GroupId = entity.GroupId,
            ArrivalDate = entity.ArrivalDate,
            Travellers = entity.Travellers?.Select(TravellerEntityMapper.ToDomain).ToList() 
                         ?? new List<Traveller>()
        };
    }
}