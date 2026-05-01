using PartyRaidR.Mobile.Api;

namespace PartyRaidR.Mobile.ViewModels
{
    public partial class CreateEventVM : BaseVM
    {
        private readonly IEventApi _eventClient;
        private readonly IPlaceApi _placeClient;

        public CreateEventVM(IEventApi eventClient, IPlaceApi placeClient)
        {
            _placeClient = placeClient;
            _eventClient = eventClient;
        }
    }
}
