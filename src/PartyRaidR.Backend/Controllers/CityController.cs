using Microsoft.AspNetCore.Mvc;
using PartyRaidR.Backend.Models;
using PartyRaidR.Backend.Services.Promises;
using PartyRaidR.Shared.Dtos;

namespace PartyRaidR.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : BaseController<City, CityDto>
    {
        private readonly ICityService _cityService;

        public CityController(ICityService service) : base(service)
        {
            _cityService = service;
        }

        [HttpGet("county/{county}")]
        public async Task<IActionResult> GetByCounty(string county) =>
            HandleResponse(await _cityService.GetByCountyAsync(county));

        [HttpGet("{id}/places/count")]
        public async Task<IActionResult> GetNumberOfPlaces(string id) =>
            HandleResponse(await _cityService.GetNumberOfPlacesAsync(id));

        [HttpGet("trending")]
        public async Task<IActionResult> GetTrendingCities() =>
            HandleResponse(await _cityService.GetTrendingCitiesAsync());
    }
}
