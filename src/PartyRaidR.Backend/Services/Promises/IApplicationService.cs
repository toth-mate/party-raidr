using PartyRaidR.Backend.Models;
using PartyRaidR.Backend.Models.Responses;
using PartyRaidR.Backend.Services.Base;
using PartyRaidR.Shared.Dtos;

namespace PartyRaidR.Backend.Services.Promises
{
    public interface IApplicationService : IBaseService<Application, ApplicationDto>
    {
        /// <summary>
        /// Get a list of applications to an event.
        /// </summary>
        /// <param name="eventId">The unique identifier of the event</param>
        /// <returns>A list of Application DTOs</returns>
        Task<ServiceResponse<List<ApplicationDto>>> GetApplicationsByEventAsync(string eventId);
        
        /// <summary>
        /// Get all applications of a user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user</param>
        /// <returns>A list of applications by the user given</returns>
        Task<ServiceResponse<List<ApplicationDto>>> GetApplicationsByUserAsync(string userId);

        /// <summary>
        /// Get the applications of an authenticated user. The user is read from the token in the request header.
        /// </summary>
        /// <returns>A list including all applications performed by the user</returns>
        Task<ServiceResponse<List<ApplicationDto>>> GetMyApplicationsAsync();
        
        /// <summary>
        /// Get the number of applications to an event.
        /// </summary>
        /// <param name="eventId">The unique identifier of the event</param>
        /// <returns>An integer representing the application count</returns>
        Task<ServiceResponse<int>> GetNumberOfApplicationsByEventAsync(string eventId);

        /// <summary>
        /// Get the number of applications the user has performed.
        /// </summary>
        /// <param name="userId">The unique identifier of the user</param>
        /// <returns>An integer representing the application count</returns>
        Task<ServiceResponse<int>> GetNumberOfApplicationsByUserAsync(string userId);

        /// <summary>
        /// Get the number of applications an authenticated user has performed. The user is read from the token in the request header.
        /// </summary>
        /// <returns>An integer representing the application count</returns>
        Task<ServiceResponse<int>> GetNumberOfMyApplicationsAsync();
        
        /// <summary>
        /// Check if a user has already applied to an event.
        /// The user is read from the token in the request header.
        /// </summary>
        /// <param name="eventId">The unique identifier of the event</param>
        /// <returns>true if the user is currently applied to the event, false otherwise</returns>
        Task<ServiceResponse<bool>> ApplicationExistsAsync(string eventId);
        
        /// <summary>
        /// Get a list of applications of an authenticated user.
        /// The user is read from the token in the request header.
        /// </summary>
        /// <returns>
        /// A list of applications. The returned objects include additional information: Event Title, Start and End Dates.
        /// </returns>
        Task<ServiceResponse<List<ApplicationDisplayDto>>> GetMyApplicationsDisplayAsync();
    }
}
