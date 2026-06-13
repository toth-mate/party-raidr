using PartyRaidR.Mobile.ViewModels;

namespace PartyRaidR.Mobile.Views.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}