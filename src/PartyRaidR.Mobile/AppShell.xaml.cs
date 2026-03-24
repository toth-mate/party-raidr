using PartyRaidR.Mobile.Views.Pages;

namespace PartyRaidR.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("eventdetails", typeof(EventDetailsPage));
        }
    }
}
