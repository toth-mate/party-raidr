using PartyRaidR.Backend.Models;
using PartyRaidR.Backend.Models.Responses;
using PartyRaidR.Backend.Services.Base;
using PartyRaidR.Shared.Dtos;

namespace PartyRaidR.Backend.Services.Promises
{
    public interface IApplicationService : IBaseService<Application, ApplicationDto>
    {
        Task<ServiceResponse<List<ApplicationDto>>> GetApplicationsByEventAsync(string eventId);
        Task<ServiceResponse<List<ApplicationDto>>> GetApplicationsByUserAsync(string userId);
        Task<ServiceResponse<int>> GetNumberOfApplicationsByEventAsync(string eventId);
    }
}
