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
        public ApplicationController(IApplicationService service) : base(service)
        {
        }
    }
}
