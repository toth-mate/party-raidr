using PartyRaidR.Backend.Models.Responses;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Dtos.AuthenticationRequests;

namespace PartyRaidR.Backend.Services.Promises
{
    public interface IUserAuthService
    {
        /// <summary>
        /// Registers a new user based on the given information. The user gets a generated unique identifier.
        /// Before inserting into the databse, the user goes through a validation process and the password is hashed for security matter.
        /// </summary>
        /// <param name="user">Information about the user to be registered</param>
        /// <returns>The new user</returns>
        Task<ServiceResponse<UserDto>> RegisterAsync(UserRegistrationDto user);

        /// <summary>
        /// Checks if the given credentials match with any existing record and generates the token if they do.
        /// </summary>
        /// <param name="user">Credentials of the user (Email Address and Password)</param>
        /// <returns>A JSON Web Token if the credentials match</returns>
        Task<ServiceResponse<string>> LoginAsync(UserLoginDto user);

        /// <summary>
        /// Checks if the given credentials match with an existing record with the role of an Admin and generates the token if they do.
        /// </summary>
        /// <param name="user">Credentials of the administrator (Email Address and Password)</param>
        /// <returns>A JSON Web Token if the credentials match</returns>
        Task<ServiceResponse<string>> AdminLoginAsync(UserLoginDto user);

        /// <summary>
        /// Get the user data after authentication. The data is read from the token in the request header.
        /// </summary>
        /// <returns>A user DTO with the authenticated user data</returns>
        Task<ServiceResponse<UserDto>> GetMeAsync();
    }
}
