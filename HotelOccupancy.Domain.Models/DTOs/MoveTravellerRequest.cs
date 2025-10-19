namespace HotelOccupancy.Domain.Models.DTOs;

public class MoveTravellerRequest
{
    public string TravellerId { get; set; }
    public string FromRoomCode { get; set; }
    public string ToRoomCode { get; set; }
    public DateOnly EffectiveDate { get; set; }
}