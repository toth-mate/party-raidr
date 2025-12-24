using Microsoft.AspNetCore.Mvc;
using PartyRaidR.Backend.Services;
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

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(UserLoginDto userLogin)
        {
            var result = await _service.LoginAsync(userLogin);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(UserRegistrationDto newUser)
        {
            var result = await _service.RegisterAsync(newUser);

            if (result.Success)
                return Ok(result);

            return StatusCode(result.StatusCode, result);
        }
    }
}
