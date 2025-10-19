using HotelOccupancy.Domain.Models;

namespace HotelOccupancy.Application;

public interface ITravellerRepository
{
    Task<List<Traveller>> GetAllAsync();
    Task<Traveller?> GetByIdAsync(Guid id);
    Task<List<Traveller>> GetByTravelGroupIdAsync(string travelGroupId);
    Task AddAsync(Traveller traveller);
}