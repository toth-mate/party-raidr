using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartyRaidR.Backend.Models;
using PartyRaidR.Backend.Services.Promises;
using PartyRaidR.Shared.Dtos;

namespace PartyRaidR.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaceController : BaseController<Place, PlaceDto>
    {
        private readonly IPlaceService _placeService;

        public PlaceController(IPlaceService service) : base(service)
        {
            _placeService = service;
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] PlaceFilterDto filter) =>
            HandleResponse(await _placeService.FilterPlacesAsync(filter));

        [Authorize]
        [HttpGet("my-places")]
        public async Task<IActionResult> GetMyPlaces() =>
            HandleResponse(await _placeService.GetMyPlacesAsync());
    }
}
