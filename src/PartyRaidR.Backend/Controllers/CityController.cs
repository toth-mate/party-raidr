using Microsoft.AspNetCore.Mvc;
using PartyRaidR.Backend.Services.Promises;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Models;

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
            HandleResponse(await _cityService.GetByCounty(county));

        [HttpGet("{id}/places/count")]
        public async Task<IActionResult> GetNumberOfPlaces(string id) =>
            HandleResponse(await _cityService.GetNumberOfPlaces(id));

        [HttpGet("trending")]
        public async Task<IActionResult> GetTrendingCities() =>
            HandleResponse(await _cityService.GetTrendingCitiesAsync());
    }
}
