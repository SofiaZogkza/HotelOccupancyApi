using HotelOccupancy.Application.Common;
using HotelOccupancy.Domain.Models;
using HotelOccupancy.Domain.Models.DTOs;

namespace HotelOccupancy.Application;

public interface IRoomService
{
    Task<Result<List<RoomOccupancyResponse>>> GetAllRoomsAsync(DateTime? date = null);
    Task<Result<RoomOccupancyResponse>> GetRoomByIdAsync(Guid roomId);
    Task<Result<RoomOccupancyResponse>> GetRoomByCodeAsync(string code);
    Task<Result<Room>> AssignTravellersToRoomAsync(AssignTravellersToRoomRequest request);
    Task<Result<Room>> MoveTravellerAsync(MoveTravellerRequest request);
}