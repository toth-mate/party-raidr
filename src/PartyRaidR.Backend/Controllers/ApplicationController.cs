using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("event/{eventId}")]
        public async Task<IActionResult> GetApplicationsByEvent(string eventId) =>
            HandleResponse(await _applicationService.GetApplicationsByEventAsync(eventId));

        [HttpGet("event/{eventId}/count")]
        public async Task<IActionResult> GetApplicationCountByEvent(string eventId) =>
            HandleResponse(await _applicationService.GetNumberOfApplicationsByEventAsync(eventId));

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetApplicationsByUser(string userId) =>
            HandleResponse(await _applicationService.GetApplicationsByUserAsync(userId));

        [HttpGet("user/{userId}/count")]
        public async Task<IActionResult> GetApplicationCountByUser(string userId) =>
            HandleResponse(await _applicationService.GetNumberOfApplicationsByUserAsync(userId));

        [Authorize]
        [HttpGet("my-applications")]
        public async Task<IActionResult> GetMyApplications() =>
            HandleResponse(await _applicationService.GetMyApplicationsAsync());

        [Authorize]
        [HttpGet("my-applications/count")]
        public async Task<IActionResult> GetMyApplicationsCount() =>
            HandleResponse(await _applicationService.GetNumberOfMyApplicationsAsync());
    }
}
