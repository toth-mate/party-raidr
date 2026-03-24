using PartyRaidR.Mobile.Views.Pages;

namespace PartyRaidR.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute("event-details", typeof(EventDetailsPage));
        }
    }
}
