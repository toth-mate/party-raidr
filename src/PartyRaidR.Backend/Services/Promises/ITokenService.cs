using PartyRaidR.Backend.Models;

namespace PartyRaidR.Backend.Services.Promises
{
    public interface ITokenService
    {
        /// <summary>
        /// Generates a JSON Web Token used for authentication.
        /// The token holds the following information: Username, Email Address, Role (regular user/admin)
        /// </summary>
        /// <param name="user">The user whose information is to be contained by the token</param>
        /// <returns>The token - a string value</returns>
        string GenerateToken(User user);
    }
}
