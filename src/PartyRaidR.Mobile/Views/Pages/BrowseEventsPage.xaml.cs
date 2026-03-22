using PartyRaidR.Mobile.ViewModels;

namespace PartyRaidR.Mobile.Views.Pages;

public partial class BrowseEventsPage : ContentPage
{
	public BrowseEventsPage(BrowseEventsVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}