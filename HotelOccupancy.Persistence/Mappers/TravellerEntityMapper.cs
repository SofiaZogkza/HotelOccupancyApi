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
            TravelGroupId = traveller.TravelGroupId
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
            TravelGroupId = entity.TravelGroupId
        };
    }
}