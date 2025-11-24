using PartyRaidR.Mobile.Models;
using PartyRaidR.Mobile.PageModels;

namespace PartyRaidR.Mobile.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}