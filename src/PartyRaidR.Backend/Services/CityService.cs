using PartyRaidR.Backend.Repos.Base;
using PartyRaidR.Backend.Repos.Promises;
using PartyRaidR.Backend.Services.Base;
using PartyRaidR.Backend.Services.Promises;
using PartyRaidR.Shared.Assemblers;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Models;

namespace PartyRaidR.Backend.Services
{
    public class CityService : BaseService<City, CityDto>, ICityService
    {
        public CityService(CityAssembler assembler, ICityRepo? repo, IUserContext userContext) : base(assembler, repo, userContext)
        {
        }
    }
}
