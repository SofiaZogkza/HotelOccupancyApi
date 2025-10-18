using HotelOccupancy.Application;
using Microsoft.AspNetCore.Mvc;

namespace HotelOccupancy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TravellersController : ControllerBase
{
    private readonly ITravellerService _travellersService;
    
    public TravellersController(ITravellerService travellersService)
    {
        _travellersService = travellersService;
    }
}