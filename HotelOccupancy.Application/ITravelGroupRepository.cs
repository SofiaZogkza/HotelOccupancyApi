using HotelOccupancy.Domain.Models;

namespace HotelOccupancy.Application;

public interface ITravelGroupRepository
{
    Task<List<TravelGroup>> GetAllAsync();
    Task<TravelGroup?> GetByIdAsync(Guid id);
    Task<TravelGroup?> GetByGroupIdAsync(string groupId); // business key
    Task AddAsync(TravelGroup travelGroup);
}