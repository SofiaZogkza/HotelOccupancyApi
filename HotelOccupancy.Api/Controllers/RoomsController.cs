using HotelOccupancy.Api.Contracts;
using HotelOccupancy.Application;
using HotelOccupancy.Domain.Models.DTOs;
using HotelOccupancy.Domain.Models.Errors;
using Microsoft.AspNetCore.Mvc;

namespace HotelOccupancy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;
    
    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(List<RoomOccupancyResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllRooms([FromQuery] DateTime? date = null)
    {
        var result = await _roomService.GetAllRoomsAsync(date);
        if (!result.IsSuccess)
            return BadRequest(new { result.ErrorCode, result.ErrorMessage });

        return Ok(result.Data);
    }
    
    [HttpGet("available")]
    [ProducesResponseType(typeof(List<RoomOccupancyResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAvailableRooms([FromQuery] DateTime? date = null)
    {
        var result = await _roomService.GetAllRoomsAsync(date);
        if (!result.IsSuccess)
            return BadRequest(new { result.ErrorCode, result.ErrorMessage });

        var availableRooms = result.Data
            .Where(r => r.AvailableBeds > 0)
            .ToList();

        return Ok(availableRooms);
    }
    
    [HttpGet("occupied")]
    [ProducesResponseType(typeof(List<RoomOccupancyResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOccupiedRooms([FromQuery] DateTime? date = null)
    {
        var result = await _roomService.GetAllRoomsAsync(date);
        if (!result.IsSuccess)
            return BadRequest(new { result.ErrorCode, result.ErrorMessage });

        var occupiedRooms = result.Data
            .Where(r => r.AvailableBeds == 0)
            .ToList();

        return Ok(occupiedRooms);
    }

    
    [HttpGet("by-id/{id}")]
    [ProducesResponseType(typeof(RoomOccupancyResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRoomById(string id)
    {
        if (!IsValidGuid(id, out var roomId))
        {
            return BadRequest(new ErrorResponse { Code = ErrorCodes.InvalidRequest, Message = ErrorMessages.InvalidGuid });
        }
        
        var result = await _roomService.GetRoomByIdAsync(roomId);
        if (!result.IsSuccess)
        {
            return NotFound(new { result.ErrorCode, result.ErrorMessage });
        }

        return Ok(result.Data);
    }
    
    [HttpGet("by-code")]
    [ProducesResponseType(typeof(RoomOccupancyResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRoomOccupancyByCode([FromQuery] RoomSearchRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.RoomCode))
        {
            return BadRequest(new ErrorResponse { Code = ErrorCodes.InvalidRequest, Message = ErrorMessages.ValueEmpty });
        }

        var result = await _roomService.GetRoomByCodeAsync(request);

        if (!result.IsSuccess)
            return NotFound(new { result.ErrorCode, result.ErrorMessage });

        return Ok(result.Data);
    }
    
    [HttpGet("by-travel-group")]
    [ProducesResponseType(typeof(List<RoomOccupancyResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetRoomsByTravelGroup([FromQuery] RoomsByTravelGroupRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.GroupId))
        {
            return BadRequest(new ErrorResponse { Code = ErrorCodes.InvalidRequest, Message = "TravelGroupId cannot be empty." });
        }

        var result = await _roomService.GetRoomsByTravelGroupAsync(request.GroupId);

        if (!result.IsSuccess)
        {
            return BadRequest(new { result.ErrorCode, result.ErrorMessage });
        }

        return Ok(result.Data);
    }

    
    [HttpPost("assign")]
    [ProducesResponseType(typeof(RoomOccupancyResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AssignTravellersToRoom([FromBody] AssignTravellersToRoomRequest request)
    {
        if (request is null)
        {
            return BadRequest(new ErrorResponse { Code = ErrorCodes.InvalidRequest, Message = ErrorMessages.InvalidRequest });
        }
        var response = await _roomService.AssignTravellersToRoomAsync(request);
        if (response == null)
            return NotFound("Room not found or could not assign travellers.");

        return Ok(response);
    }

    [HttpPost("move")]
    [ProducesResponseType(typeof(RoomOccupancyResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> MoveTraveller([FromBody] MoveTravellerRequest request)
    {
        if (request is null)
        {
            return BadRequest(new ErrorResponse { Code = ErrorCodes.InvalidRequest, Message = ErrorMessages.InvalidRequest });
        }
        
        var response = await _roomService.MoveTravellerAsync(request);
        if (!response.IsSuccess)
        {
            return BadRequest(new { response.ErrorCode, response.ErrorMessage });

        }
        return Ok(response.Data);
    }

    public static bool IsValidGuid(string value, out Guid guid) =>
        Guid.TryParse(value, out guid) && guid != Guid.Empty;
}