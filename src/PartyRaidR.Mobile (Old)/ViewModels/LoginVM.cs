using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PartyRaidR.Mobile.Api;
using PartyRaidR.Mobile.Services;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Dtos.AuthenticationRequests;
using System.Diagnostics;
using System.Text.Json;

namespace PartyRaidR.Mobile.ViewModels
{
    public partial class LoginVM : BaseVM
    {
        private readonly IAuthService _authService;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        public LoginVM(IAuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand(CanExecute = nameof(IsNotBusy))]
        private async Task Login()
        {
            // Commands can only be executed if 'not busy' to avoid button spamming.
            IsBusy = true;
            LoginCommand.NotifyCanExecuteChanged();

            try
            {
                await _authService.Login(Email, Password);
                await Shell.Current.GoToAsync("//home");
            }
            catch(Exception ex) { Debug.WriteLine($"FAIL: {ex.Message}"); }
            finally
            {
                IsBusy = false;
                LoginCommand.NotifyCanExecuteChanged();
            }
        }
    }
}
