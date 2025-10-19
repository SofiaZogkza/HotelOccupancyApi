namespace HotelOccupancy.Domain.Models;

public class TravelGroup
{
    public Guid Id { get; set; }
    public string GroupId { get; set; }      // 6-char, max 2 letters
    public DateTime ArrivalDate { get; set; }
    public List<Traveller> Travellers { get; set; }
}