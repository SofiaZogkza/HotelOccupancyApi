namespace HotelOccupancy.Domain.Models;

public class Room
{
    public Guid Id { get; set; }
    public string Code { get; set; }         // 4-digit room code
    public int BedCount { get; set; }
    public List<Traveller> AssignedTravellers { get; set; } = new();
}