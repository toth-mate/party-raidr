using PartyRaidR.Mobile.ViewModels;

namespace PartyRaidR.Mobile.Views.Pages;

public partial class EventDetailsPage : ContentView
{
	public EventDetailsPage(EventDetailsVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}