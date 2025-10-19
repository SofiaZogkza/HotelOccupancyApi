using HotelOccupancy.Application.Common;
using HotelOccupancy.Domain.Models;

namespace HotelOccupancy.Application;

public interface IRoomRepository
{
    Task<Result<List<Room>>> GetAllAsync(DateTime? date);
    Task<Result<Room>> GetByIdAsync(Guid id);
    Task<Result<Room>> GetByCodeAsync(string code);
    Task<Result<List<Room>>> GetRoomsByTravelGroupAsync(string travelGroupId);
    Task<Result<Room>> UpdateAsync(Room room);
}