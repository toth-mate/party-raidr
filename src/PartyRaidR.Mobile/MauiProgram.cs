using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using PartyRaidR.Mobile.Api;
using PartyRaidR.Mobile.Services;
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
            builder.Services.AddTransient<EventDetailsVM>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<BrowseEventsPage>();
            builder.Services.AddTransient<EventDetailsPage>();

            builder.Services.AddSingleton<IAuthService, AuthService>();

            builder.Services.AddTransient<AuthHandler>();

            // API Clients
            builder.Services.AddRefitClient<IEventApi>()
                            .AddRefitClient<IAuthApi>()
                            .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://10.0.2.2:8080/api"))
                            .AddHttpMessageHandler<AuthHandler>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
