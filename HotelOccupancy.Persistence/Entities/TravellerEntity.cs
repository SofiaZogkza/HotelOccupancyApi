namespace HotelOccupancy.Persistence.Entities;

public class TravellerEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    
    internal Guid RoomId { get; set; }  // we don't need it in the domain model - stays only here in persistence
    public RoomEntity Room { get; set; }

    public string TravelGroupId { get; set; }
}