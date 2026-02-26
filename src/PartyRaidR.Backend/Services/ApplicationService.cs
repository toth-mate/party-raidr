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
        private readonly IApplicationRepo _applicationRepo;
        private readonly IEventRepo _eventRepo;
        private readonly IEventService _eventService;

        public ApplicationService(ApplicationAssembler? assembler, IApplicationRepo? repo, IUserContext? userContext, IEventRepo? eventRepo, IEventService? eventService) : base(assembler, repo, userContext)
        {
            _applicationRepo = repo ?? throw new ArgumentNullException(nameof(repo));
            _eventRepo = eventRepo ?? throw new ArgumentNullException(nameof(eventRepo), "Event repository cannot be null.");
            _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
        }

        public async Task<ServiceResponse<List<ApplicationDto>>> GetApplicationsByEventAsync(string eventId)
        {
            try
            {
                var eventResponse = await _eventService.GetByIdAsync(eventId);

                if (eventResponse.Success)
                {
                    var applications = await _repo.FindByConditionAsync(a => a.EventId == eventId);
                    List<ApplicationDto> result = applications.Select(_assembler.ConvertToDto).ToList();

                    return CreateResponse(true, 200, result);
                }
                else
                    return CreateResponse<List<ApplicationDto>>(false, eventResponse.StatusCode, message: eventResponse.Message);           
            }
            catch (Exception ex)
            {
                return CreateResponse<List<ApplicationDto>>(false, 500, message: $"An error occurred while retrieving applications for the event: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<List<ApplicationDto>>> GetApplicationsByUserAsync(string userId)
        {
            try
            {
                var applications = await _applicationRepo.GetApplicationsByUserAsync(userId);
                List<ApplicationDto> result = applications.Select(_assembler.ConvertToDto).ToList();

                return CreateResponse(true, 200, result);
            }
            catch (Exception ex)
            {
                return CreateResponse<List<ApplicationDto>>(false, 500, message: $"An error occurred while retrieving applications for the user: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<List<ApplicationDto>>> GetMyApplicationsAsync() =>
            await GetApplicationsByUserAsync(_userContext.UserId);

        public async Task<ServiceResponse<int>> GetNumberOfApplicationsByEventAsync(string eventId)
        {
            try
            {
                var eventResponse = await _eventService.GetByIdAsync(eventId);

                if (eventResponse.Success)
                {
                    int result = await _repo.CountAsync(a => a.EventId == eventId);

                    return CreateResponse(true, 200, result);
                }

                return CreateResponse<int>(false, eventResponse.StatusCode, message: eventResponse.Message);
            }
            catch (Exception ex)
            {
                return CreateResponse<int>(false, 500, message: $"An error occurred while retrieving the number of applications for the event: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<int>> GetNumberOfApplicationsByUserAsync(string userId)
        {
            try
            {
                int count = await _repo.CountAsync(a => a.UserId == userId);
                return CreateResponse(true, 200, count);
            }
            catch(Exception ex)
            {
                return CreateResponse<int>(false, 500, message: $"An error occurred while retrieving the number of applications for the user: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<int>> GetNumberOfMyApplicationsAsync() =>
            await GetNumberOfApplicationsByUserAsync(_userContext.UserId);

        public override async Task<ServiceResponse<ApplicationDto>> AddAsync(ApplicationDto dto)
        {
            try
            {
                Event? @event = await _eventRepo.GetByIdAsync(dto.EventId);
                
                if(@event is null)
                    return CreateResponse<ApplicationDto>(false, 404, message: "Event not found.");

                // Check if the user is trying to apply to their own event
                else if(_userContext.UserId == @event.AuthorId)
                    return CreateResponse<ApplicationDto>(false, 400, message: "You cannot apply to your own event.");

                // Duplicate applications are not allowed
                bool applicationExists = await _applicationRepo!.ApplicationExistsAsync(_userContext.UserId, dto.EventId);

                if (applicationExists)
                    return CreateResponse<ApplicationDto>(false, 409, message: "You have already applied to this event.");

                // Forbid applying to live events
                var now = DateTime.Now;
                bool isEventLive = @event.StartingDate >= now && now <= @event.EndingDate;

                if (isEventLive)
                    return CreateResponse<ApplicationDto>(false, 400, message: "Applying to live events is not possible.");

                // Check current participant number
                int numberOfApplicants = await _repo.CountAsync(a => a.EventId == @event.Id);

                if (@event.Room != 0 && numberOfApplicants >= @event.Room)
                    return CreateResponse<ApplicationDto>(false, 400, message: "The number of applicants to this event have already reached the max room.");
            }
            catch(Exception ex)
            {
                return CreateResponse<ApplicationDto>(false, 500, message: $"An error occurred while validating the application: {ex.Message}");
            }

            dto.UserId = _userContext.UserId;
            dto.TimeOfApplication = DateTime.Now;

            return await base.AddAsync(dto);
        }

        public override async Task<ServiceResponse<ApplicationDto>> UpdateAsync(ApplicationDto dto)
        {
            try
            {
                Application? application = await _applicationRepo.GetApplicationWithEventAsync(dto.Id);

                if(application is null)
                    return CreateResponse<ApplicationDto>(false, 404, message: "Application not found.");

                if (application.Event.AuthorId != _userContext.UserId)
                    return CreateResponse<ApplicationDto>(false, 403, message: "Only the author of the event can update the status of the application.");

                application.Status = dto.Status;
                await _applicationRepo.SaveChangesAsync();

                return CreateResponse<ApplicationDto>(true, 204);
            }
            catch (Exception ex)
            {
                return CreateResponse<ApplicationDto>(false, 500, message: $"An error occurred while updating the application: {ex.Message}");
            }
        }

        public override async Task<ServiceResponse<ApplicationDto>> DeleteAsync(string id)
        {
            try
            {
                Application? application = await _applicationRepo.GetApplicationWithEventAsync(id);

                if(application is null)
                    return CreateResponse<ApplicationDto>(false, 404, message: "Application not found.");

                if(application.UserId == _userContext.UserId || application.Event.AuthorId == _userContext.UserId)
                {
                    _repo.Delete(application);
                    await _repo.SaveChangesAsync();

                    return CreateResponse<ApplicationDto>(true, 204);
                }
                else
                    return CreateResponse<ApplicationDto>(false, 403, message: "Only the applicant or the author of the target event can delete the application.");
            }
            catch (Exception ex)
            {
                return CreateResponse<ApplicationDto>(false, 500, message: $"An error occurred while deleting the application: {ex.Message}");
            }
        }
    }
}
