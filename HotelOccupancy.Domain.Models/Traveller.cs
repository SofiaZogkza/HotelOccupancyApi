namespace HotelOccupancy.Domain.Models;

public class Traveller
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string TravelGroupId { get; set; }
    
}