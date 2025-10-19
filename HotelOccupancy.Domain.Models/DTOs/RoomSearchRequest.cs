namespace HotelOccupancy.Domain.Models.DTOs;

public class RoomSearchRequest
{
    public string RoomCode { get; set; }
    public string? Date { get; set; }
}