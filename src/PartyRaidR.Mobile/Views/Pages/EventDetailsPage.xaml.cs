using PartyRaidR.Mobile.ViewModels;

namespace PartyRaidR.Mobile.Views.Pages;

public partial class EventDetailsPage : ContentPage
{
	public EventDetailsPage(EventDetailsVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

		if(BindingContext is EventDetailsVM vm)
		{
			if(vm.LoadEventCommand.CanExecute(null))
				await vm.LoadEventCommand.ExecuteAsync(null);
		}
    }
}