using Microsoft.AspNetCore.Mvc;
using PartyRaidR.Backend.Services.Base;
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
        public async Task<IActionResult> GetByCountyAsync(string county)
        {
            var response = await _cityService.GetByCounty(county);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}/places/count")]
        public async Task<IActionResult> GetNumberOfPlacesAsync(string id)
        {
            var response = await _cityService.GetNumberOfPlaces(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("trending")]
        public async Task<IActionResult> GetTrendingCitiesAsync()
        {
            var response = await _cityService.GetTrendingCitiesAsync();
            return StatusCode(response.StatusCode, response);
        }
    }
}
