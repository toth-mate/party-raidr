using PartyRaidR.Mobile.ViewModels;

namespace PartyRaidR.Mobile.Views.Pages;

public partial class CreateEventPage : ContentPage
{
	public CreateEventPage(CreateEventVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}