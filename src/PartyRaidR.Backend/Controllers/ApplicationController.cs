using Microsoft.AspNetCore.Mvc;
using PartyRaidR.Backend.Models;
using PartyRaidR.Backend.Services.Promises;
using PartyRaidR.Shared.Dtos;

namespace PartyRaidR.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : BaseController<Application, ApplicationDto>
    {
        public readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService service) : base(service)
        {
            _applicationService = service;
        }

        [HttpGet("event")]
        public async Task<IActionResult> GetApplicationsByEvent(string eventId) =>
            HandleResponse(await _applicationService.GetApplicationsByEventAsync(eventId));
    }
}
