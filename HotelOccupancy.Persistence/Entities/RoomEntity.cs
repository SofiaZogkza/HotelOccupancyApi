namespace HotelOccupancy.Persistence.Entities;

public class RoomEntity
{
    public Guid Id { get; set; }
    public string Code { get; set; } // 4-digit code
    public int BedCount { get; set; }

    // Navigation
    public List<TravellerEntity> AssignedTravellers { get; set; } = new();

}