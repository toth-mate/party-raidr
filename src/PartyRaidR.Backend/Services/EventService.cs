using Microsoft.EntityFrameworkCore;
using PartyRaidR.Backend.Assemblers;
using PartyRaidR.Backend.Exceptions;
using PartyRaidR.Backend.Models;
using PartyRaidR.Backend.Models.Responses;
using PartyRaidR.Backend.Repos.Promises;
using PartyRaidR.Backend.Services.Base;
using PartyRaidR.Backend.Services.Promises;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Enums;

namespace PartyRaidR.Backend.Services
{
    public partial class EventService : BaseService<Event, EventDto>, IEventService
    {
        private readonly IEventRepo _eventRepo;
        private readonly IPlaceRepo _placeRepo;
        private readonly ICityRepo _cityRepo;
        private readonly IUserService _userService;

        public EventService(EventAssembler assembler, IEventRepo? repo, IUserContext? userContext, IPlaceRepo? placeRepo, IUserService? userService, ICityRepo? cityRepo) : base(assembler, repo, userContext)
        {
            _eventRepo = repo ?? throw new ArgumentNullException(nameof(IEventRepo));
            _placeRepo = placeRepo ?? throw new ArgumentNullException(nameof(IPlaceRepo));
            _userService = userService ?? throw new ArgumentNullException(nameof(IUserService));
            _cityRepo = cityRepo ?? throw new ArgumentNullException(nameof(ICityRepo));
        }

        public async Task<ServiceResponse<EventDisplayDto>> GetEventWithDetailsAsync(string id)
        {
            try
            {
                Event @event = await _eventRepo.GetEventWithDisplayData(id);
                EventDisplayDto result = new EventDisplayDto
                {
                    Id = @event.Id,
                    Title = @event.Title,
                    Description = @event.Description,
                    StartingDate = GetDateDisplayString(@event.StartingDate),
                    EndingDate = GetDateDisplayString(@event.EndingDate),
                    City = @event.Place.City.Name,
                    PlaceName = @event.Place.Name,
                    Category = nameof(@event.Category),
                    AuthorName = @event.User.Username,
                    Room = @event.Room,
                    TicketPrice = @event.TicketPrice,
                    DateCreated = GetDateDisplayString(@event.DateCreated),
                    IsActive = @event.IsActive,
                    EventStatus = GetEventStatusDisplayName(@event.StartingDate, @event.EndingDate)
                };

                return CreateResponse(true, 200, result);
            }
            catch(EntityNotFoundException ex)
            {
                return CreateResponse<EventDisplayDto>(false, 404, message: ex.Message);
            }
            catch(Exception ex)
            {
                return CreateResponse<EventDisplayDto>(false, 500, message: $"An error occured while retrieving the event: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<List<EventDisplayDto>>> GetEventsWithDetailsAsync()
        {
            try
            {
                List<Event> events = await _eventRepo.GetEventsWithDisplayData();
                List<EventDisplayDto> result = events.Select(e => new EventDisplayDto
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    StartingDate = GetDateDisplayString(e.StartingDate),
                    EndingDate = GetDateDisplayString(e.EndingDate),
                    City = e.Place.City.Name,
                    PlaceName = e.Place.Name,
                    Category = GetEventCategoryDisplayName(e.Category),
                    AuthorName = e.User.Username,
                    Room = e.Room,
                    TicketPrice = e.TicketPrice,
                    DateCreated = GetDateDisplayString(e.DateCreated),
                    IsActive = e.IsActive,
                    EventStatus = GetEventStatusDisplayName(e.StartingDate, e.EndingDate)
                }).ToList();

                return CreateResponse(true, 200, result);
            }
            catch (Exception ex)
            {
                return CreateResponse<List<EventDisplayDto>>(false, 500, message: $"An error occured while retrieving events: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<int>> GetNumberOfEventsAsync()
        {
            try
            {
                int count = await _repo.CountAsync();
                return CreateResponse(true, 200, count);
            }
            catch (Exception ex)
            {
                return CreateResponse<int>(false, 500, message: $"An error occured while counting events: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<int>> GetNumberOfActiveEventsAsync()
        {
            try
            {
                int count = await _repo.CountAsync(e => e.IsActive);
                return CreateResponse(true, 200, count);
            }
            catch (Exception ex)
            {
                return CreateResponse<int>(false, 500, message: $"An error occured while counting active events: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<int>> GetNumberOfArchivedEventsAsync()
        {
            try
            {
                int count = await _repo.CountAsync(e => !e.IsActive);
                return CreateResponse(true, 200, count);
            }
            catch (Exception ex)
            {
                return CreateResponse<int>(false, 500, message: $"An error occured while counting archived events: {ex.Message}");
            }
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

                return CreateResponse(true, 200, result, result.Count == 0 ? "No upcoming events found." : string.Empty);
            }
            catch (Exception ex)
            {
                return CreateResponse<List<UpcomingEventDto>>(false, 500, message: $"An error occured while retrieving upcoming events: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<List<EventDto>>> GetEventsByUserIdAsync(string userId)
        {
            try
            {
                List<Event> events = await _eventRepo.GetEventsByUserIdAsync(userId);
                List<EventDto> result = events.Select(_assembler.ConvertToDto).ToList();
                return CreateResponse(true, 200, result, result.Count == 0 ? "No events found for the specified user." : string.Empty);
            }
            catch (Exception ex)
            {
                return CreateResponse<List<EventDto>>(false, 500, message: $"An error occured while retrieving events: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<List<EventDto>>> GetMyEventsAsync()
        {
            string userId = _userContext.UserId;
            return await GetEventsByUserIdAsync(userId);
        }

        public async Task<ServiceResponse<List<EventDto>>> FilterEventsAsync(EventFilterDto filter)
        {
            try
            {
                List<Event> events = await _eventRepo.FilterEventsAsync(filter.Title,
                                                                        filter.Description,
                                                                        filter.StartingDate,
                                                                        filter.EndingDate,
                                                                        filter.PlaceName,
                                                                        filter.PlaceId,
                                                                        filter.CityId,
                                                                        filter.Category,
                                                                        filter.TicketPriceMin,
                                                                        filter.TicketPriceMax);
                List<EventDto> result = events.Select(_assembler.ConvertToDto).ToList();

                return CreateResponse(true, 200, result, result.Count == 0 ? "No events found matching the specified criteria." : string.Empty);
            }
            catch (Exception ex)
            {
                return CreateResponse<List<EventDto>>(false, 500, message: $"An error occured while filtering events: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<EventDto>> ArchiveOldEventsAsync()
        {
            try
            {
                List<Event> events = await _eventRepo.GetAllAsQueryable().Where(e => e.EndingDate < DateTime.UtcNow && e.IsActive).ToListAsync();

                foreach (Event eventToArchive in events)
                {
                    eventToArchive.IsActive = false;
                    _repo.Update(eventToArchive);
                    await _repo.SaveChangesAsync();
                }

                return CreateResponse<EventDto>(true, 200, message: events.Count == 0 ? "No old events to archive." : $"{events.Count} event(s) archived successfully.");
            }
            catch(Exception ex)
            {
                return CreateResponse<EventDto>(false, 500, message: $"An error occured while archiving old events: {ex.Message}");
            }
        }

        public override async Task<ServiceResponse<EventDto>> AddAsync(EventDto dto)
        {
            try
            {
                // Check if the new event is valid
                await ValidateEvent(dto);
            }
            catch(OverlappingEventsException oee)
            {
                return CreateResponse<EventDto>(false, 409, message: oee.Message);
            }
            catch (Exception ex)
            {
                return CreateResponse<EventDto>(false, 500, message: $"An error occured while validating the event: {ex.Message}");
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
                    return CreateResponse<EventDto>(false, 404, message: "Event not found.");
                }
                else if (!eventToEdit.IsActive)
                {
                    return CreateResponse<EventDto>(false, 400, message: "Cannot edit an archived event.");
                }
                else
                {
                    bool isUserAuthor = await IsUserAuthor(eventToEdit);

                    if (!isUserAuthor)
                        return CreateResponse<EventDto>(false, 403, message: "You do not have permission to edit this event.");

                    if(dto.EventStatus != 0)
                        return CreateResponse<EventDto>(false, 400, message: "Events with the flag 'Live', 'Starting Soon' or 'Past' can not be edited.");

                    EventDto updated = dto;
                    updated.AuthorId = eventToEdit.AuthorId;

                    await ValidateEvent(updated);
                }
            }
            catch (Exception ex)
            {
                return CreateResponse<EventDto>(false, 500, message: $"An error occured while validating the event: {ex.Message}");
            }

            return await base.UpdateAsync(dto);
        }

        public override async Task<ServiceResponse<EventDto>> DeleteAsync(string id)
        {
            try
            {
                Event? eventToDelete = await _repo.GetByIdAsync(id);

                if (eventToDelete is null)
                    return CreateResponse<EventDto>(false, 404, message: "Event not found.");
                else
                {
                    bool isUserAuthor = await IsUserAuthor(eventToDelete);

                    if (!isUserAuthor)
                        return CreateResponse<EventDto>(false, 403, message: "You do not have permission to delete this event.");
                }
            }
            catch (Exception ex)
            {
                return CreateResponse<EventDto>(false, 500, message: $"An error occured while verifying user permissions: {ex.Message}");
            }

            return await base.DeleteAsync(id);
        }

        public async Task<ServiceResponse<List<EventDto>>> GetActiveEventsAsync()
        {
            try
            {
                var events = await _repo.FindByConditionAsync(e => e.IsActive);
                List<EventDto> result = events.Select(_assembler.ConvertToDto).ToList();

                return CreateResponse(true, 200, result, result.Count == 0 ? "No active events found." : string.Empty);
            }
            catch(Exception ex)
            {
                return CreateResponse<List<EventDto>>(false, 500, message: $"An error occured while retrieving active events: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<List<EventDto>>> GetArchivedEventsAsync()
        {
            try
            {
                var events = await _repo.FindByConditionAsync(e => !e.IsActive);
                List<EventDto> result = events.Select(_assembler.ConvertToDto).ToList();

                return CreateResponse(true, 200, result, result.Count == 0 ? "No archived events found." : string.Empty);
            }
            catch (Exception ex)
            {
                return CreateResponse<List<EventDto>>(false, 500, message: $"An error occured while retrieving archived events: {ex.Message}");
            }
        }
    }
}
