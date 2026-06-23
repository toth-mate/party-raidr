using PartyRaidR.Backend.Models;
using PartyRaidR.Backend.Models.Responses;
using PartyRaidR.Backend.Services.Base;
using PartyRaidR.Shared.Dtos;

namespace PartyRaidR.Backend.Services.Promises
{
    public interface IEventService : IBaseService<Event, EventDto>
    {
        /// <summary>
        /// Inserts a city into the database.
        /// Prior to the insertion, the event is validated.
        /// If the event overlaps with another one (start date - end date), the execution is terminated.
        /// </summary>
        /// <param name="dto">An object including the information to insert</param>
        /// <returns>An event DTO object with the validated information</returns>
        Task<ServiceResponse<EventDto>> AddAsync(CreateEventDto dto);

        /// <summary>
        /// Get an event with information ready for UI display.
        /// </summary>
        /// <param name="id">The unique identifier of the event</param>
        /// <returns>A display DTO of the event with the given ID</returns>
        Task<ServiceResponse<EventDisplayDto>> GetEventWithDetailsAsync(string id);

        /// <summary>
        /// Get a list of all the events with information ready for UI display.
        /// </summary>
        /// <returns>A list of display DTOs</returns>
        Task<ServiceResponse<List<EventDisplayDto>>> GetEventsWithDetailsAsync();

        /// <summary>
        /// Get a list of active events.
        /// </summary>
        /// <returns>A list of all the active events</returns>
        Task<ServiceResponse<List<EventDto>>> GetActiveEventsAsync();

        /// <summary>
        /// Get a list of archived events. An event is considered archived if it is NOT ACTIVE.
        /// </summary>
        /// <returns>A list of all the events that have been archived</returns>
        Task<ServiceResponse<List<EventDto>>> GetArchivedEventsAsync();

        /// <summary>
        /// Get a list of upcoming events. An event is considered upcoming if the start date is in the future.
        /// </summary>
        /// <returns>A list of events with UI-ready display information</returns>
        Task<ServiceResponse<List<UpcomingEventDto>>> GetUpcomingEventsAsync();

        /// <summary>
        /// Get events created by a user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user</param>
        /// <returns>A list of events created by the user</returns>
        Task<ServiceResponse<List<EventDto>>> GetEventsByUserIdAsync(string userId);

        /// <summary>
        /// Get events created by an authenticated user. The user is read from the token in the request header.
        /// </summary>
        /// <returns>A list of events created by the user</returns>
        Task<ServiceResponse<List<EventDto>>> GetMyEventsAsync();

        /// <summary>
        /// Get a list of events based on a filter.
        /// </summary>
        /// <param name="filter">The object containing the filtering parameters</param>
        /// <returns>The list of events meeting the conditions provided</returns>
        Task<ServiceResponse<List<EventDisplayDto>>> FilterEventsAsync(EventFilterDto filter);

        /// <summary>
        /// Marks events that have already ended "not active".
        /// </summary>
        /// <returns>null with a message stating whether any events have been archived during the process</returns>
        Task<ServiceResponse<EventDto>> ArchiveOldEventsAsync();

        /// <summary>
        /// Get the number of all events.
        /// </summary>
        /// <returns>An integer representing the number of events</returns>
        Task<ServiceResponse<int>> GetNumberOfEventsAsync();

        /// <summary>
        /// Get the number of all active events.
        /// </summary>
        /// <returns>An integer representing the number of events that are active</returns>
        Task<ServiceResponse<int>> GetNumberOfActiveEventsAsync();

        /// <summary>
        /// Get the number of all archived events
        /// </summary>
        /// <returns>An integer representing the number of events that are not active</returns>
        Task<ServiceResponse<int>> GetNumberOfArchivedEventsAsync();
    }
}
