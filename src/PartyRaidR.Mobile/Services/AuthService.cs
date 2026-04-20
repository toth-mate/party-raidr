using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Maui.Graphics.Text;
using PartyRaidR.Mobile.Api;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Dtos.AuthenticationRequests;
using System.Diagnostics;
using System.Text.Json;

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
            var snackbarOptions = new SnackbarOptions
            {
                BackgroundColor = Color.FromArgb("#7852d1"),
                TextColor = Color.FromRgb(255, 255, 255),
                ActionButtonTextColor = Color.FromRgb(255, 255, 255),
                CharacterSpacing = .115
            };
            string snackbarText = string.Empty;

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
                    await SaveUser(user);

                    snackbarText = $"Successfully logged in as {user?.Username}!";
                }
            }
            catch (Exception ex)
            {
                snackbarOptions.BackgroundColor = Color.FromRgb(207, 23, 53);
                snackbarText = "Login failed. Please check your credentials and try again.";
            }
            finally
            {
                var snackbar = Snackbar.Make(snackbarText, visualOptions: snackbarOptions);
                await snackbar.Show();
            }
        }

        public void Logout() =>
            Preferences.Default.Remove("user");

        private async Task SaveUser(UserDto? user)
        {
            // The user info is converted to a string to make storing easier.
            if (user is not null)
            {
                string userJson = JsonSerializer.Serialize(user);
                Preferences.Default.Set("user", userJson);
            }
        }
    }
}
