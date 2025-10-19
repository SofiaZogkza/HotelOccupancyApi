namespace HotelOccupancy.Domain.Models.DTOs;

public class AssignTravellersToRoomRequest
{
    public string RoomCode { get; set; }
    public List<string> TravellerIds { get; set; }
}