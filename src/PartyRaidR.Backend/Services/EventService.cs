using Microsoft.EntityFrameworkCore;
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
        private readonly IEventRepo _eventRepo;
        private readonly IPlaceRepo _placeRepo;
        private readonly ICityRepo _cityRepo;
        private readonly IUserService _userService;

        public EventService(EventAssembler assembler, IEventRepo? repo, IUserContext userContext, IPlaceRepo? placeRepo, IUserService? userService, ICityRepo? cityRepo) : base(assembler, repo, userContext)
        {
            _eventRepo = repo ?? throw new ArgumentNullException(nameof(IEventRepo));
            _placeRepo = placeRepo ?? throw new ArgumentNullException(nameof(IPlaceRepo));
            _userService = userService ?? throw new ArgumentNullException(nameof(IUserService));
            _cityRepo = cityRepo ?? throw new ArgumentNullException(nameof(ICityRepo));
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

        public override async Task<ServiceResponse<EventDto>> DeleteAsync(string id)
        {
            try
            {
                Event? eventToDelete = await _repo.GetByIdAsync(id);

                if (eventToDelete is null)
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
                    bool isUserAuthor = await IsUserAuthor(eventToDelete);

                    if (!isUserAuthor)
                    {
                        return new ServiceResponse<EventDto>
                        {
                            Success = false,
                            StatusCode = 403,
                            Message = "You do not have permission to delete this event."
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new ServiceResponse<EventDto>
                {
                    Success = false,
                    Message = $"An error occured while verifying user permissions: {ex.Message}",
                    StatusCode = 500
                };
            }

            return await base.DeleteAsync(id);
        }

        public async Task<ServiceResponse<List<UpcomingEventDto>>> GetUpcomingEventsAsync()
        {
            try
            {
                DateTime now = DateTime.UtcNow;

                var query = from e in _eventRepo.GetAllAsQueryable()
                            join p in _placeRepo.GetAllAsQueryable() on e.PlaceId equals p.Id
                            join c in _cityRepo.GetAllAsQueryable() on p.CityId equals c.Id
                            where e.StartingDate > now
                            orderby e.StartingDate
                            select new UpcomingEventDto
                            {
                                Id = e.Id,
                                Title = e.Title,
                                CityName = c.Name,
                                PlaceName = p.Name,
                                StartTime = e.StartingDate
                            };

                List<UpcomingEventDto> result = await query.Take(10).ToListAsync();

                return new ServiceResponse<List<UpcomingEventDto>>
                {
                    Data = result,
                    Success = true,
                    Message = result.Count == 0 ? "No upcoming events found." : string.Empty,
                    StatusCode = 200
                };
            }
            catch(Exception ex)
            {
                return new ServiceResponse<List<UpcomingEventDto>>
                {
                    Data = null,
                    Success = false,
                    Message = $"An error occured while retrieving upcoming events: {ex.Message}",
                    StatusCode = 500
                };
            }
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

        public async Task<ServiceResponse<List<EventDto>>> GetEventsByUserIdAsync(string userId)
        {
            try
            {
                List<Event> events = await _eventRepo.GetEventsByUserIdAsync(userId);
                List<EventDto> result = events.Select(_assembler.ConvertToDto).ToList();

                return new ServiceResponse<List<EventDto>>
                {
                    Data = result,
                    Success = true,
                    Message = result.Count == 0 ? "No events found for the specified user." : string.Empty,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<EventDto>>
                {
                    Data = null,
                    Success = false,
                    Message = $"An error occured while retrieving events: {ex.Message}",
                    StatusCode = 500
                };
            }
        }

        public async Task<ServiceResponse<List<EventDto>>> GetMyEventsAsync()
        {
            string userId = _userContext.UserId;
            return await GetEventsByUserIdAsync(userId);
        }
    }
}
