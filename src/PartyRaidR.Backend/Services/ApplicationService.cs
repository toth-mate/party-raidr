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

        public ApplicationService(ApplicationAssembler? assembler, IApplicationRepo? repo, IUserContext userContext, IEventRepo? eventRepo, IEventService? eventService) : base(assembler, repo, userContext)
        {
            _applicationRepo = repo ?? throw new ArgumentNullException(nameof(repo));
            _eventRepo = eventRepo ?? throw new ArgumentNullException(nameof(eventRepo), "Event repository cannot be null.");
            _eventService = eventService ?? throw new ArgumentNullException(nameof(_eventService));
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

                    return new ServiceResponse<List<ApplicationDto>>
                    {
                        Success = true,
                        Data = result,
                        StatusCode = 200
                    };
                }
                else
                {
                    return new ServiceResponse<List<ApplicationDto>>
                    {
                        Success = false,
                        Message = eventResponse.Message,
                        StatusCode = eventResponse.StatusCode
                    };
                }                
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<ApplicationDto>>
                {
                    Success = false,
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }

        public async Task<ServiceResponse<int>> GetNumberOfApplicationsByEventAsync(string eventId)
        {
            try
            {
                var eventResponse = await _eventService.GetByIdAsync(eventId);

                if (eventResponse.Success)
                {
                    int result = await _repo.CountAsync(a => a.EventId == eventId);

                    return new ServiceResponse<int>
                    {
                        Success = true,
                        Data = result,
                        StatusCode = 200
                    };
                }
                else
                {
                    return new ServiceResponse<int>
                    {
                        Success = false,
                        Message = eventResponse.Message,
                        StatusCode = eventResponse.StatusCode
                    };
                }
            }
            catch (Exception ex)
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
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
                // Check if the user is trying to apply to their own event
                else if(_userContext.UserId == @event.AuthorId)
                {
                    return new ServiceResponse<ApplicationDto>
                    {
                        Success = false,
                        Message = "You cannot apply to your own event.",
                        StatusCode = 400
                    };
                }

                // Duplicate applications are not allowed
                bool applicationExists = await _applicationRepo!.ApplicationExistsAsync(_userContext.UserId, dto.EventId);

                if (applicationExists)
                {
                    return new ServiceResponse<ApplicationDto>
                    {
                        Success = false,
                        Message = "You have already applied to this event.",
                        StatusCode = 409
                    };
                }

                // Forbid applying to live events
                var now = DateTime.Now;
                bool isEventLive = @event.StartingDate >= now && now <= @event.EndingDate;

                if (isEventLive)
                {
                    return new ServiceResponse<ApplicationDto>
                    {
                        Success = false,
                        Message = "Applying to live events is not possible.",
                        StatusCode = 400
                    };
                }

                // Check current participant number
                int numberOfApplicants = await _repo.CountAsync(a => a.EventId == @event.Id);

                if (@event.Room != 0 && numberOfApplicants >= @event.Room)
                {
                    return new ServiceResponse<ApplicationDto>
                    {
                        Success = false,
                        Message = "The number of applicants to this event have already reached the max room.",
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

        public override async Task<ServiceResponse<ApplicationDto>> UpdateAsync(ApplicationDto dto)
        {
            try
            {
                Application? application = await _applicationRepo.GetApplicationWithEventAsync(dto.Id);

                if(application is null)
                {
                    return new ServiceResponse<ApplicationDto>
                    {
                        Success = false,
                        Message = "Application not found.",
                        StatusCode = 404
                    };
                }

                if (application.Event.AuthorId != _userContext.UserId)
                {
                    return new ServiceResponse<ApplicationDto>
                    {
                        Success = false,
                        Message = "Only the author of the event can update the status of the application.",
                        StatusCode = 403
                    };
                }

                application.Status = dto.Status;
                await _applicationRepo.SaveChangesAsync();

                return new ServiceResponse<ApplicationDto>
                {
                    Success = true,
                    StatusCode = 204
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<ApplicationDto>
                {
                    Success = false,
                    Message = $"An error occurred while updating the application: {ex.Message}",
                    StatusCode = 500
                };
            }
        }

        public override async Task<ServiceResponse<ApplicationDto>> DeleteAsync(string id)
        {
            try
            {
                Application? application = await _applicationRepo.GetApplicationWithEventAsync(id);

                if(application is null)
                {
                    return new ServiceResponse<ApplicationDto>
                    {
                        Success = false,
                        Message = "Application not found",
                        StatusCode = 404
                    };
                }

                if(application.UserId == _userContext.UserId || application.Event.AuthorId == _userContext.UserId)
                {
                    _repo.Delete(application);
                    await _repo.SaveChangesAsync();

                    return new ServiceResponse<ApplicationDto>
                    {
                        Success = true,
                        StatusCode = 204
                    };
                }
                else
                {
                    return new ServiceResponse<ApplicationDto>
                    {
                        Success = false,
                        Message = "Only the applicant or the author of the target event can delete the application.",
                        StatusCode = 400
                    };
                }
            }
            catch (Exception ex)
            {
                return new ServiceResponse<ApplicationDto>
                {
                    Success = false,
                    Message = $"An error occured while deleting the application: {ex.Message}",
                    StatusCode = 500
                };
            }
        }
    }
}
