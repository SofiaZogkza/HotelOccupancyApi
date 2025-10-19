namespace HotelOccupancy.Domain.Models.DTOs;

public class TravellerResponse
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string TravelGroupId { get; set; }
    public DateTime ArrivalDate { get; set; } 
}