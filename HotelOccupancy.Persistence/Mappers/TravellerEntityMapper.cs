using HotelOccupancy.Domain.Models;
using HotelOccupancy.Persistence.Entities;

namespace HotelOccupancy.Persistence.Mappers;

public class TravellerEntityMapper
{
    public static TravellerEntity ToEntity(Traveller traveller)
    {
        return new TravellerEntity
        {
            Id = traveller.Id,
            FirstName = traveller.FirstName,
            LastName = traveller.LastName,
            DateOfBirth = traveller.DateOfBirth,
            TravelGroupId = traveller.TravelGroupId,
            TravelGroup = traveller.TravelGroup != null
                ? new TravelGroupEntity
                {
                    GroupId = traveller.TravelGroup.GroupId,
                    ArrivalDate = traveller.TravelGroup.ArrivalDate
                }
                : null
        };
    }

    public static Traveller ToDomain(TravellerEntity entity)
    {
        return new Traveller
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            DateOfBirth = entity.DateOfBirth,
            TravelGroupId = entity.TravelGroupId,
            TravelGroup = entity.TravelGroup != null
                ? new TravelGroup
                {
                    Id = entity.TravelGroup.Id,
                    GroupId = entity.TravelGroup.GroupId,
                    ArrivalDate = entity.TravelGroup.ArrivalDate
                }
                : null
        };
    }
}