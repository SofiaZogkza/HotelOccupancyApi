using HotelOccupancy.Application;
using Microsoft.AspNetCore.Mvc;

namespace HotelOccupancy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TravelGroupsController : ControllerBase
{
    private readonly ITravelGroupService _travelGroupService;
    
    public TravelGroupsController(ITravelGroupService travelGroupService)
    {
        _travelGroupService = travelGroupService;
    }
}