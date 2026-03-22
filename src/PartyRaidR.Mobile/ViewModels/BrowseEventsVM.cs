using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PartyRaidR.Mobile.Api;
using PartyRaidR.Shared.Dtos;
using System.Collections.ObjectModel;

namespace PartyRaidR.Mobile.ViewModels
{
    public partial class BrowseEventsVM : BaseVM
    {
        private readonly IEventApi _eventApi;

        [ObservableProperty]
        private ObservableCollection<EventDisplayDto> _events;

        [ObservableProperty]
        private string _message;

        public BrowseEventsVM(IEventApi eventApi)
        {
            _eventApi = eventApi;
            Events = [];
            Message = "Hello!";
        }

        [RelayCommand]
        private async Task LoadEvents()
        {
            try
            {
                IsBusy = true;

                var response = await _eventApi.DisplayAll();
                if (response is not null)
                    Events = new ObservableCollection<EventDisplayDto>(response);
            }
            catch (Exception)
            {
            }
            finally { IsBusy = false; }
        }
    }
}
