using HotelOccupancy.Application.Common;
using HotelOccupancy.Application.Mappers;
using HotelOccupancy.Domain.Models;
using HotelOccupancy.Domain.Models.DTOs;

namespace HotelOccupancy.Application.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;
    private readonly ITravellerRepository _travellerRepository;
    private readonly ITravelGroupRepository _travelGroupRepository;

    public RoomService(
        IRoomRepository roomRepository,
        ITravellerRepository travellerRepository,
        ITravelGroupRepository travelGroupRepository)
    {
        _roomRepository = roomRepository;
        _travellerRepository = travellerRepository;
        _travelGroupRepository = travelGroupRepository;
    }
    
    public async Task<Result<List<RoomOccupancyResponse>>> GetAllRoomsAsync(DateTime? date = null)
    {
        var roomsResult = await _roomRepository.GetAllAsync(date);
        if (!roomsResult.IsSuccess)
        {
            return Result<List<RoomOccupancyResponse>>
                .Failure(roomsResult.ErrorCode!, roomsResult.ErrorMessage!);
        }
        
        var toResponse = DtoDomainMapper.ToResponseList(roomsResult.Data!);

        return Result<List<RoomOccupancyResponse>>.Success(toResponse);
    }
    public async Task<Result<RoomOccupancyResponse>> GetRoomByIdAsync(Guid roomId)
    {
        var result = await _roomRepository.GetByIdAsync(roomId);

        if (!result.IsSuccess)
        {
            return Result<RoomOccupancyResponse>.Failure(result.ErrorCode, result.ErrorMessage);
        }
        
        var response = DtoDomainMapper.ToResponse(result.Data!);
        
        return Result<RoomOccupancyResponse>.Success(response);
    }
    
    public async Task<Result<RoomOccupancyResponse>> GetRoomByCodeAsync(string code)
    {
        var result = await _roomRepository.GetByCodeAsync(code);

        if (!result.IsSuccess)
        {
            return Result<RoomOccupancyResponse>.Failure(result.ErrorCode, result.ErrorMessage);
        }
        
        var response = DtoDomainMapper.ToResponse(result.Data!);
        
        return Result<RoomOccupancyResponse>.Success(response);
    }

    public async Task<Result<Room>> AssignTravellersToRoomAsync(AssignTravellersToRoomRequest request)
    {
        var roomResult = await _roomRepository.GetByCodeAsync(request.RoomCode);
        if (!roomResult.IsSuccess)
        {
            return Result<Room>.Failure(roomResult.ErrorCode!, roomResult.ErrorMessage!);
        }

        var room = roomResult.Data!;

        // Add travellers
        foreach (var traveller in request.TravellerIds)
        {
            if (!room.AssignedTravellers.Any(t => t.Id == traveller.Id))
                room.AssignedTravellers.Add(new Traveller { Id = traveller.Id });
        }

        return await _roomRepository.UpdateAsync(room);
    }

    public async Task<Result<Room>> MoveTravellerAsync(MoveTravellerRequest request)
    {
        var fromRoomResult = await _roomRepository.GetByCodeAsync(request.FromRoomCode);
        if (!fromRoomResult.IsSuccess)
            return Result<Room>.Failure(fromRoomResult.ErrorCode!, fromRoomResult.ErrorMessage!);

        var toRoomResult = await _roomRepository.GetByCodeAsync(request.ToRoomCode);
        if (!toRoomResult.IsSuccess)
            return Result<Room>.Failure(toRoomResult.ErrorCode!, toRoomResult.ErrorMessage!);

        var fromRoom = fromRoomResult.Data!;
        var toRoom = toRoomResult.Data!;

        if (!Guid.TryParse(request.TravellerId, out var travellerIdGuid))
            return Result<Room>.InvalidRequest("TravellerId is not a valid GUID");

        var traveller = fromRoom.AssignedTravellers
            .FirstOrDefault(t => t.Id == travellerIdGuid);

        if (traveller == null)
            return Result<Room>.NotFound();

        // Move traveller
        fromRoom.AssignedTravellers.Remove(traveller);
        toRoom.AssignedTravellers.Add(traveller);

        await _roomRepository.UpdateAsync(fromRoom);
        await _roomRepository.UpdateAsync(toRoom);

        // Return the room where the traveller was moved
        return Result<Room>.Success(toRoom);
    }
}