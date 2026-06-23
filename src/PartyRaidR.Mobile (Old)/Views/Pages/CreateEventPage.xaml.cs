using PartyRaidR.Mobile.ViewModels;

namespace PartyRaidR.Mobile.Views.Pages;

public partial class CreateEventPage : ContentPage
{
	public CreateEventPage(CreateEventVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

		if(BindingContext is CreateEventVM vm)
		{
			if (vm.LoadPlacesCommand.CanExecute(null))
				await vm.LoadPlacesCommand.ExecuteAsync(null);
		}
    }
}