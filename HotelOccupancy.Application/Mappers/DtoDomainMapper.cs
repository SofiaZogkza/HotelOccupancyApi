using HotelOccupancy.Domain.Models;
using HotelOccupancy.Domain.Models.DTOs;

namespace HotelOccupancy.Application.Mappers;

public static class DtoDomainMapper
{
    public static RoomOccupancyResponse ToResponse(Room room)
    {
        return new RoomOccupancyResponse
        {
            Id = room.Id.ToString(),
            Code = room.Code,
            BedCount = room.BedCount,            
            AssignedTravellers = room.AssignedTravellers
                .Select(t => new TravellerResponse
                {
                    Id = t.Id.ToString(),
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    DateOfBirth = t.DateOfBirth,
                    TravelGroupId = t.TravelGroupId,
                    ArrivalDate = t.TravelGroup != null 
                        ? t.TravelGroup.ArrivalDate 
                        : default(DateTime)  // fallback if TravelGroup is null
                }).ToList(),
            //AvailableBeds = room.BedCount - room.AssignedTravellers.Count
        };
    }
    
    public static List<RoomOccupancyResponse> ToResponseList(List<Room> rooms)
    {
        return rooms.Select(room => ToResponse(room)).ToList();
    }
    
    public static TravellerResponse ToResponse(this Traveller traveller)
    {
        return new TravellerResponse
        {
            Id = traveller.Id.ToString(),
            FirstName = traveller.FirstName,
            LastName = traveller.LastName,
            DateOfBirth = traveller.DateOfBirth,
            TravelGroupId = traveller.TravelGroupId,
            ArrivalDate = traveller.TravelGroup != null 
                ? traveller.TravelGroup.ArrivalDate 
                : default(DateTime) 
        };
    }
}