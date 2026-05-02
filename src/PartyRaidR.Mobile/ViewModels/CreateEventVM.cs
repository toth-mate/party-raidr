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
        private ObservableCollection<EventCategory> _categories;

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private string _description;

        [ObservableProperty]
        private DateOnly _startDate;

        [ObservableProperty]
        private DateOnly _endDate;

        [ObservableProperty]
        private TimeOnly _startTime;

        [ObservableProperty]
        private TimeOnly _endTime;

        [ObservableProperty]
        private PlaceDto _selectedPlace;

        [ObservableProperty]
        private EventCategory _selectedCategory;

        [ObservableProperty]
        private int _maxGuests;

        [ObservableProperty]
        private decimal _price;

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

        [RelayCommand]
        private async Task SavePlace()
        {
            IsBusy = true;
            try
            {
                EventDto newEvent = new EventDto
                {
                    Title = Title,
                    Description = Description,
                    StartingDate = StartDate.ToDateTime(StartTime),
                    EndingDate = EndDate.ToDateTime(EndTime),
                    PlaceId = SelectedPlace.Id,
                    Category = SelectedCategory,
                    Room = MaxGuests,
                    TicketPrice = Price
                };

                Debug.WriteLine("\n------------------------------------");
                Debug.WriteLine(newEvent.Title);
                Debug.WriteLine(newEvent.Description);
                Debug.WriteLine(newEvent.StartingDate);
                Debug.WriteLine(newEvent.EndingDate);
                Debug.WriteLine(newEvent.PlaceId);
                Debug.WriteLine(newEvent.Category);
                Debug.WriteLine(newEvent.Room);
                Debug.WriteLine(newEvent.TicketPrice);
                Debug.WriteLine("------------------------------------\n");
            }
            catch(Exception ex)
            { Debug.WriteLine($"FAIL: {ex.Message}"); }
            finally { IsBusy = false; }
        }
    }
}
