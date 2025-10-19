namespace HotelOccupancy.Domain.Models.DTOs;

public class RoomOccupancyResponse
{
    public string Id { get; set; }
    public string Code { get; set; }
    public int BedCount { get; set; }
    public List<TravellerResponse> AssignedTravellers { get; set; }
    public int AvailableBeds => BedCount - AssignedTravellers.Count;
}