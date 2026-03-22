using PartyRaidR.Mobile.ViewModels;

namespace PartyRaidR.Mobile.Views.Pages;

public partial class BrowseEventsPage : ContentPage
{
	public BrowseEventsPage(BrowseEventsVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        if(BindingContext is BrowseEventsVM vm)
        {
            if(vm.LoadEventsCommand.CanExecute(null))
                await vm.LoadEventsCommand.ExecuteAsync(null);
        }
    }
}