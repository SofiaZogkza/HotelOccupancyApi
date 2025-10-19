namespace HotelOccupancy.Persistence.Entities;

public class TravelGroupEntity
{
    public Guid Id { get; set; }
    public string GroupId { get; set; }
    public DateTime ArrivalDate { get; set; }

    public List<TravellerEntity> Travellers { get; set; }
}