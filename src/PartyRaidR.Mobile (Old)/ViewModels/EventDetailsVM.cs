using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PartyRaidR.Mobile.Api;
using PartyRaidR.Shared.Dtos;

namespace PartyRaidR.Mobile.ViewModels
{
    [QueryProperty(nameof(Id), "id")]
    public partial class EventDetailsVM : BaseVM
    {
        private readonly IEventApi _eventApi;

        [ObservableProperty]
        private string _id;

        [ObservableProperty]
        private EventDisplayDto _event;

        public EventDetailsVM(IEventApi eventApi)
        {
            _eventApi = eventApi;
            Event = new EventDisplayDto();
        }

        [RelayCommand]
        private async Task LoadEvent()
        {
            if(Id is not null)
                Event = await _eventApi.GetEventDisplay(Id);
        }
    }
}
