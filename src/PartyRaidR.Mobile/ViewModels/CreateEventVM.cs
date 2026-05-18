//using Android.Webkit;
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
        private DateTime _startDate;

        [ObservableProperty]
        private DateTime _endDate;

        [ObservableProperty]
        private TimeSpan _startTime;

        [ObservableProperty]
        private TimeSpan _endTime;

        [ObservableProperty]
        private PlaceDto _selectedPlace;

        [ObservableProperty]
        private EventCategory _selectedCategory;

        [ObservableProperty]
        private int _maxGuests;

        [ObservableProperty]
        private decimal _price;

        public DateTime Now { get; } = DateTime.Now;

        public CreateEventVM(IEventApi eventClient, IPlaceApi placeClient)
        {
            _placeClient = placeClient;
            _eventClient = eventClient;
            Places = new ObservableCollection<PlaceDto>();
            SelectedPlace = new PlaceDto();
            this.Categories = new ObservableCollection<EventCategory>(Enum.GetValues(typeof(EventCategory)).Cast<EventCategory>().ToList());
            StartDate = DateTime.Now;
            EndDate = DateTime.Now.AddDays(1);
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
                Debug.WriteLine($"Start date: {StartDate}, Time: {StartTime}");
                Debug.WriteLine($"End date: {EndDate}, Time: {EndTime}");

                DateTime startDate = new DateTime(new DateOnly(StartDate.Year, StartDate.Month, StartDate.Day), new TimeOnly(StartTime.Hours, StartTime.Minutes, StartTime.Seconds)),
                         endTime   = new DateTime(new DateOnly(EndDate.Year, EndDate.Month, EndDate.Day), new TimeOnly(EndTime.Hours, EndTime.Minutes, EndTime.Seconds));

                CreateEventDto newEvent = new CreateEventDto()
                {
                    Title = Title,
                    Description = Description,
                    StartingDate = startDate,
                    EndingDate = endTime,
                    PlaceId = SelectedPlace.Id,
                    Category = GetCategoryName(SelectedCategory),
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

                var response = await _eventClient.CreateEvent(newEvent);
                Debug.WriteLine(response.ToString());
                await Shell.Current.GoToAsync($"//home");
            }
            catch(Exception ex)
            { Debug.WriteLine($"FAIL: {ex.Message}, Source: {ex.Source}"); }
            finally { IsBusy = false; }
        }

        private string GetCategoryName(EventCategory category)
        {
            switch (category)
            {
                case EventCategory.OutdoorsActivity:
                    return "Outdoors Activity";
                case EventCategory.IndoorsActivity:
                    return "Indoors Activity";
                case EventCategory.Concert:
                    return "Concert";
                case EventCategory.Festival:
                    return "Festival";
                case EventCategory.Party:
                    return "Party";
                default:
                    return "None";
            }
        }
    }
}
