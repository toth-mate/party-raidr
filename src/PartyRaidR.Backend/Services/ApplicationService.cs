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
        private readonly IEventRepo _eventRepo;

        public ApplicationService(ApplicationAssembler? assembler, IApplicationRepo? repo, IEventRepo eventRepo, IUserContext userContext) : base(assembler, repo, userContext)
        {
            _eventRepo = eventRepo ?? throw new ArgumentNullException(nameof(eventRepo), "Event repository cannot be null.");
        }

        public override async Task<ServiceResponse<ApplicationDto>> AddAsync(ApplicationDto dto)
        {
            try
            {
                Event? @event = await _eventRepo.GetByIdAsync(dto.EventId);
                
                if(@event is null)
                {
                    return new ServiceResponse<ApplicationDto>
                    {
                        Success = false,
                        Message = "Event not found.",
                        StatusCode = 404
                    };
                }
                else if(_userContext.UserId == @event.AuthorId)
                {
                    return new ServiceResponse<ApplicationDto>
                    {
                        Success = false,
                        Message = "You cannot apply to your own event.",
                        StatusCode = 400
                    };
                }
            }
            catch(Exception ex)
            {
                return new ServiceResponse<ApplicationDto>
                {
                    Success = false,
                    Message = $"An error occurred while adding the application: {ex.Message}",
                    StatusCode = 500
                };
            }

            dto.UserId = _userContext.UserId;
            dto.TimeOfApplication = DateTime.Now;

            return await base.AddAsync(dto);
        }
    }
}
