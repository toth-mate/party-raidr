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
        private IApplicationRepo _applicationRepo;
        private readonly IEventRepo _eventRepo;

        public ApplicationService(ApplicationAssembler? assembler, IApplicationRepo? repo, IUserContext userContext, IEventRepo? eventRepo) : base(assembler, repo, userContext)
        {
            _applicationRepo = repo ?? throw new ArgumentNullException(nameof(repo));
            _eventRepo = eventRepo ?? throw new ArgumentNullException(nameof(eventRepo), "Event repository cannot be null.");
        }

        public async Task<ServiceResponse<List<ApplicationDto>>> GetApplicationsByEventAsync(string eventId)
        {
            try
            {
                Event? @event = await _eventRepo.GetByIdAsync(eventId);

                if(@event is null)
                {
                    return new ServiceResponse<List<ApplicationDto>>
                    {
                        Success = false,
                        Message = "Event with the given ID was not found.",
                        StatusCode = 404
                    };
                }

                var applications = await _repo.FindByConditionAsync(a => a.EventId == eventId);
                List<ApplicationDto> result = applications.Select(_assembler.ConvertToDto).ToList();

                return new ServiceResponse<List<ApplicationDto>>
                {
                    Success = true,
                    Data = result,
                    StatusCode = 200
                };
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

                bool applicationExists = await _applicationRepo!.ApplicationExistsAsync(_userContext.UserId, dto.EventId);

                if (applicationExists)
                {
                    return new ServiceResponse<ApplicationDto>
                    {
                        Success = false,
                        Message = "You have already applied to this event.",
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
