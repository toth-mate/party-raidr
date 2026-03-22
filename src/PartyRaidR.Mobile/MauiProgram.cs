using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using PartyRaidR.Mobile.Api;
using PartyRaidR.Mobile.ViewModels;
using PartyRaidR.Mobile.Views.Pages;
using Refit;

namespace PartyRaidR.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Font Awesome 7 Brands-Regular-400.otf", "FA-Brands");
                    fonts.AddFont("Font Awesome 7 Free-Regular-400.otf", "FA-Reg");
                    fonts.AddFont("Font Awesome 7 Free-Solid-900.otf", "FA-Solid");
                });

            builder.Services.AddTransient<MainVM>();
            builder.Services.AddTransient<LoginVM>();
            builder.Services.AddTransient<BrowseEventsVM>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<BrowseEventsPage>();

            // API Clients
            builder.Services.AddRefitClient<IEventApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:8080/api"));

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
