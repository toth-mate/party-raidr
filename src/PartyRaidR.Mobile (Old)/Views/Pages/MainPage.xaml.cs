using PartyRaidR.Mobile.ViewModels;

namespace PartyRaidR.Mobile.Views.Pages;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage(MainVM vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private void OnCounterClicked(object? sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}
