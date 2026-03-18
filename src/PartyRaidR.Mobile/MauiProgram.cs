using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using PartyRaidR.Mobile.ViewModels;
using PartyRaidR.Mobile.Views.Pages;

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
                });

            builder.Services.AddTransient<MainVM>();
            builder.Services.AddTransient<LoginVM>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<LoginPage>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
