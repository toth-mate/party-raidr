using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartyRaidR.Backend.Services.Base;
using PartyRaidR.Backend.Services.Promises;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Models;

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
        public async Task<IActionResult> FilterAsync([FromQuery] PlaceFilterDto filter)
        {
            var result = await _placeService.FilterPlacesAsync(filter);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize]
        [HttpGet("my-places")]
        public async Task<IActionResult> GetMyPlacesAsync()
        {
            var result = await _placeService.GetMyPlacesAsync();
            return StatusCode(result.StatusCode, result);
        }
    }
}
