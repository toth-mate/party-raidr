using PartyRaidR.Backend.Repos.Promises;
using PartyRaidR.Backend.Services.Base;
using PartyRaidR.Backend.Services.Promises;
using PartyRaidR.Shared.Assemblers;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Models;
using PartyRaidR.Shared.Models.Responses;

namespace PartyRaidR.Backend.Services
{
    public class EventService : BaseService<Event, EventDto>, IEventService
    {
        private readonly IPlaceRepo _placeRepo;
        private readonly IUserService _userService;

        public EventService(EventAssembler assembler, IEventRepo? repo, IUserContext userContext, IPlaceRepo? placeRepo, IUserService? userService) : base(assembler, repo, userContext)
        {
            _placeRepo = placeRepo ?? throw new ArgumentNullException(nameof(IPlaceRepo));
            _userService = userService ?? throw new ArgumentNullException(nameof(IUserService));
        }

        public override async Task<ServiceResponse<EventDto>> AddAsync(EventDto dto)
        {
            try
            {
                // Check if the new event is valid
                await ValidateNewEvent(dto);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<EventDto>
                {
                    Data = null,
                    Success = false,
                    Message = ex.Message,
                    StatusCode = 400
                };
            }

            return await base.AddAsync(dto);
        }

        public override async Task<ServiceResponse<EventDto>> UpdateAsync(EventDto dto)
        {
            try
            {
                Event? eventToEdit = await _repo.GetByIdAsync(dto.Id);

                if (eventToEdit is null)
                {
                    return new ServiceResponse<EventDto>
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = "Event not found."
                    };
                }
                else
                {
                    bool isUserAuthor = await IsUserAuthor(eventToEdit);

                    if (!isUserAuthor)
                    {
                        return new ServiceResponse<EventDto>
                        {
                            Success = false,
                            StatusCode = 403,
                            Message = "You do not have permission to edit this event."
                        };
                    }

                    EventDto updated = dto;
                    updated.AuthorId = eventToEdit.AuthorId;
                }
            }
            catch (Exception ex)
            {
                return new ServiceResponse<EventDto>
                {
                    Success = false,
                    Message = ex.Message,
                    StatusCode = 400
                };
            }

            return await base.UpdateAsync(dto);
        }

        private async Task ValidateNewEvent(EventDto dto)
        {
            if (dto.Title.Trim() == string.Empty)
                throw new ArgumentException("Title cannot be empty.");

            if (dto.Description.Trim() == string.Empty)
                throw new ArgumentException("Description cannot be empty.");

            if (dto.StartingDate >= dto.EndingDate)
                throw new ArgumentException("Starting date must be before ending date.");

            if (dto.StartingDate <= DateTime.UtcNow)
                throw new ArgumentException("Starting date must be in the future.");

            if (dto.TicketPrice < 0)
                throw new ArgumentException("Ticket price cannot be negative.");

            Place? place = await _placeRepo.GetByIdAsync(dto.PlaceId);

            if (dto.PlaceId is null || dto.PlaceId == string.Empty || place is null)
                throw new ArgumentException("Invalid place.");

            if (dto.Room < 0 || dto.Room == 1)
                throw new ArgumentException("Room must be greater than 1.");

            dto.DateCreated = DateTime.UtcNow;
            dto.AuthorId = _userContext.UserId;
        }

        private async Task<bool> IsUserAuthor(Event eventToEdit)
        {
            string userId = _userContext.UserId;
            var userResult = await _userService.GetByIdAsync(userId);

            return (userResult.Success && userResult.Data is not null) && (userResult.Data.Role != UserRole.Admin && eventToEdit.AuthorId == userId);
        }
    }
}
