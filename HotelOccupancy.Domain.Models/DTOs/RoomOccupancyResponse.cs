namespace HotelOccupancy.Domain.Models.DTOs;

public class RoomOccupancyResponse
{
    public string RoomCode { get; set; }
    public int BedCount { get; set; }
    public List<Traveller> AssignedTravellers { get; set; }
    public int AvailableBeds => BedCount - AssignedTravellers.Count;
}