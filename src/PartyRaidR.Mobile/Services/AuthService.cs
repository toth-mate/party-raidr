using System.Text.Json;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Mobile.Api;
using System.Diagnostics;
using PartyRaidR.Shared.Dtos.AuthenticationRequests;

namespace PartyRaidR.Mobile.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthApi _authClient;

        public AuthService(IAuthApi authApi)
        {
            _authClient = authApi;
        }

        public UserDto? User
        {
            get
            {
                string userJson = Preferences.Default.Get("user", string.Empty);

                if (!string.IsNullOrEmpty(userJson))
                    return JsonSerializer.Deserialize<UserDto>(userJson);

                return null;
            }
        }

        public bool IsLoggedIn => User is not null;

        public async Task Login(string email, string password)
        {
            try
            {
                UserLoginDto creds = new UserLoginDto
                {
                    Email = email,
                    Password = password
                };

                string token = await _authClient.Login(creds);
                Debug.WriteLine(token);

                if(!string.IsNullOrEmpty(token))
                {
                    await SecureStorage.SetAsync("access_token", token);

                    UserDto? user = await _authClient.GetMe();

                    // The user info is converted to a string to make storing easier.
                    if (user is not null)
                    {
                        string userJson = JsonSerializer.Serialize(user);
                        Preferences.Default.Set("user", userJson);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"FAIL: {ex.Message}");
            }
        }

        public void Logout() =>
            Preferences.Default.Remove("user");
    }
}
