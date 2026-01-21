using PartyRaidR.Backend.Services.Promises;

namespace PartyRaidR.Backend.Services
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _accessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _accessor = httpContextAccessor;
        }

        public string UserId {
            get
            {
                var id = _accessor.HttpContext?.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                        ?? _accessor.HttpContext?.User.FindFirst("sub")?.Value
                        ?? _accessor.HttpContext?.User.FindFirst("nameid")?.Value
                        ?? _accessor.HttpContext?.User.FindFirst("uid")?.Value
                        ?? _accessor.HttpContext?.User.FindFirst("user_id")?.Value
                        ?? _accessor.HttpContext?.User.FindFirst("id")?.Value
                        ?? Guid.Empty.ToString();
                var user = _accessor.HttpContext?.User;

                if(user != null)
                {
                    foreach (var claim in user.Claims)
                    {
                        // You can log or inspect claims here if needed
                        // For example: Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
                        Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
                    }
                }

                return id;
            }
        }
            
    }
}
