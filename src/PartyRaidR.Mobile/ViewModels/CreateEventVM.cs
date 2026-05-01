using Android.Webkit;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PartyRaidR.Mobile.Api;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Enums;
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

        [ObservableProperty]
        private PlaceDto _selectedPlace;

        [ObservableProperty]
        private ObservableCollection<EventCategory> _categories;

        public CreateEventVM(IEventApi eventClient, IPlaceApi placeClient)
        {
            _placeClient = placeClient;
            _eventClient = eventClient;
            Places = new ObservableCollection<PlaceDto>();
            SelectedPlace = new PlaceDto();
            this.Categories = new ObservableCollection<EventCategory>(Enum.GetValues(typeof(EventCategory)).Cast<EventCategory>().ToList());
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
            finally { IsBusy = false; }
        }
    }
}
