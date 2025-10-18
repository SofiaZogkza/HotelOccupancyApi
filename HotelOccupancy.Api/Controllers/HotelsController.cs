using HotelOccupancy.Application;
using Microsoft.AspNetCore.Mvc;

namespace HotelOccupancy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HotelsController : ControllerBase
{
    private readonly IHotelService _hotelService;
    
    public HotelsController(IHotelService hotelService)
    {
        _hotelService = hotelService;
    }
}