using HotelOccupancy.Domain.Models;

namespace HotelOccupancy.Application;

public interface IRoomRepository
{
    Task<List<Room>> GetAllAsync(DateTime? date);
    Task<Room?> GetByIdAsync(Guid id);
    Task AddAsync(Room room);
}