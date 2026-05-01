using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PartyRaidR.Mobile.Api;
using PartyRaidR.Shared.Dtos;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace PartyRaidR.Mobile.ViewModels
{
    public partial class CreateEventVM : BaseVM
    {
        private readonly IEventApi _eventClient;
        private readonly IPlaceApi _placeClient;

        [ObservableProperty]
        private ObservableCollection<PlaceDto> _places;

        public CreateEventVM(IEventApi eventClient, IPlaceApi placeClient)
        {
            _placeClient = placeClient;
            _eventClient = eventClient;
            Places = new ObservableCollection<PlaceDto>();
        }

        [RelayCommand]
        private async Task LoadPlaces()
        {
            IsBusy = true;
            try
            {
                List<PlaceDto> places = await _placeClient.GetPlaces();
                Places = new ObservableCollection<PlaceDto>(places);
            }
            catch(Exception ex)
            { Debug.WriteLine($"FAIL: {ex.Message}"); }
            finally { IsBusy = false;  }
        }
    }
}
