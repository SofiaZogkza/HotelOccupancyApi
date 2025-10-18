using HotelOccupancy.Application;
using Microsoft.AspNetCore.Mvc;

namespace HotelOccupancy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;
    
    public RoomsController(IRoomService roomService)
    {
        _roomService = _roomService;
    }
}