using PartyRaidR.Backend.Models;

namespace PartyRaidR.Backend.Services.Promises
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
