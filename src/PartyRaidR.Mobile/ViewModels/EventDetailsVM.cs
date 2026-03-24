using CommunityToolkit.Mvvm.ComponentModel;
using PartyRaidR.Shared.Dtos;

namespace PartyRaidR.Mobile.ViewModels
{
    [QueryProperty(nameof(Id), "id")]
    public partial class EventDetailsVM : BaseVM
    {
        [ObservableProperty]
        private string _id;

        public EventDetailsVM()
        {
        }
    }
}
