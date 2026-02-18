using PartyRaidR.Backend.Assemblers;
using PartyRaidR.Backend.Models;
using PartyRaidR.Backend.Models.Responses;
using PartyRaidR.Backend.Repos.Promises;
using PartyRaidR.Backend.Services.Base;
using PartyRaidR.Backend.Services.Promises;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Enums;

namespace PartyRaidR.Backend.Services
{
    public class ApplicationService : BaseService<Application, ApplicationDto>, IApplicationService
    {
        public ApplicationService(ApplicationAssembler? assembler, IApplicationRepo? repo, IUserContext userContext) : base(assembler, repo, userContext)
        {
        }
    }
}
