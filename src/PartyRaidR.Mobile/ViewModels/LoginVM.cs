using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PartyRaidR.Mobile.Api;
using PartyRaidR.Shared.Dtos.AuthenticationRequests;
using System.Diagnostics;

namespace PartyRaidR.Mobile.ViewModels
{
    public partial class LoginVM : BaseVM
    {
        private readonly IAuthApi _authClient;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        public LoginVM(IAuthApi authClient)
        {
            _authClient = authClient;
        }

        [RelayCommand]
        private async Task Login()
        {
            UserLoginDto creds = new UserLoginDto
            {
                Email = Email,
                Password = Password
            };

            string result = await _authClient.Login(creds);
            Debug.WriteLine(result);

            if(!string.IsNullOrEmpty(result))
            {
                await SecureStorage.SetAsync("access_token", result);
                await Shell.Current.GoToAsync("//home");
            }
        }
    }
}
