using Microsoft.AspNetCore.Mvc;
using PartyRaidR.Backend.Models.Responses;
using PartyRaidR.Backend.Services.Promises;
using PartyRaidR.Shared.Dtos.AuthenticationRequests;

namespace PartyRaidR.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserAuthService _service;

        public AuthController(IUserAuthService authService)
        {
            _service = authService;
        }

        private IActionResult HandleResponse<T>(ServiceResponse<T> response)
        {
            if (response.Success)
                return response.Data is null ? NoContent() : StatusCode(response.StatusCode, response.Data);

            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLogin) =>
            HandleResponse(await _service.LoginAsync(userLogin));

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationDto newUser) =>
            HandleResponse(await _service.RegisterAsync(newUser));
    }
}
