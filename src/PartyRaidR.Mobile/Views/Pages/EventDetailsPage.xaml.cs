using PartyRaidR.Mobile.ViewModels;

namespace PartyRaidR.Mobile.Views.Pages;

public partial class EventDetailsPage : ContentPage
{
	public EventDetailsPage(EventDetailsVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}