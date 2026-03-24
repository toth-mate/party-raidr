using CommunityToolkit.Mvvm.ComponentModel;
using PartyRaidR.Shared.Dtos;

namespace PartyRaidR.Mobile.ViewModels
{
    [QueryProperty(nameof(Event.Id), "id")]
    public partial class EventDetailsVM : BaseVM
    {
        [ObservableProperty]
        private EventDisplayDto _event;

        public EventDetailsVM()
        {
        }
    }
}
