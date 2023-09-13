using CommunityToolkit.Maui;
using MauiVideo.Services;
using MauiVideo.ViewModels;
using MauiVideo.Views;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;

namespace MauiVideo;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            // Initialize the .NET MAUI Community Toolkit by adding the below line of code
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<ITmdbService, TmdbService>();
        builder.Services.AddSingleton<ISecretService, SecretService>();


        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainPageViewModel>();


        builder.Services.AddSingleton<CategoriesPage>();
        builder.Services.AddSingleton<CategoriesViewModel>();


        builder.Services.AddTransientWithShellRoute<DetailsPage, DetailsViewModel>(nameof(DetailsPage));
        builder.Services.AddTransientWithShellRoute<MovieListPage, MovieListViewModel>(nameof(MovieListPage));


        Routing.RegisterRoute(nameof(CategoriesPage), typeof(CategoriesPage));


        builder.Services.AddHttpClient(TmdbService.TmdbHttpClientName);

        return builder.Build();
    }
}
