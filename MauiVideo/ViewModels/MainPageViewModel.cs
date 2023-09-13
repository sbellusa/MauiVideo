using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiVideo.Models;
using MauiVideo.Services;
using MauiVideo.Views;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiVideo.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        private ITmdbService TmdbService { get; }

        public MainPageViewModel(ITmdbService tmdbService)
        {
            TmdbService = tmdbService;
        }

        [ObservableProperty]
        Movie trendingMovie;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsVisibleInfoBox))]
        Movie selectedMovie;
        public bool IsVisibleInfoBox => SelectedMovie is not null;

        public ObservableCollection<Movie> Trending { get; set; } = new();
        public ObservableCollection<Movie> TopRated { get; set; } = new();
        public ObservableCollection<Movie> NetflixOriginals { get; set; } = new();
        public ObservableCollection<Movie> ActionMovies { get; set; } = new();

        bool hasInitialised;

        public async Task InitialiseAsync()
        {
            if (hasInitialised)
            {
                RandomiseHeader();
                return;
            }

            Dispatcher.GetForCurrentThread()?.Dispatch(async () => await DispatchInitialise());

            hasInitialised = true;
        }

        private async Task DispatchInitialise()
        {
            var trendingList = await TmdbService.GetTrendingAsync();

            if (trendingList.IsSuccessStatusCode && trendingList?.Results?.Any() == true)
            {
                Trending.Clear();
                foreach (var result in trendingList.Results) { Trending.Add(result); }

                RandomiseHeader();
            }

            var netflixList = await TmdbService.GetNetflixOriginalsAsync();

            if (netflixList.IsSuccessStatusCode && netflixList?.Results?.Any() == true)
            {
                NetflixOriginals.Clear();
                foreach (var result in netflixList.Results) { NetflixOriginals.Add(result); }
            }


            var topRatedList = await TmdbService.GetTopRatedAsync();

            if (topRatedList.IsSuccessStatusCode && topRatedList?.Results?.Any() == true)
            {
                TopRated.Clear();
                foreach (var result in topRatedList.Results) { TopRated.Add(result); }
            }


            var actionMoviesList = await TmdbService.GetActionAsync();

            if (actionMoviesList.IsSuccessStatusCode && actionMoviesList?.Results?.Any() == true)
            {
                ActionMovies.Clear();
                foreach (var result in actionMoviesList.Results) { ActionMovies.Add(result); }
            }
        }

        private void RandomiseHeader()
        {
            if (Trending?.Count > 0)
            {
                TrendingMovie = Trending.OrderBy(x => Guid.NewGuid())
                                        .FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.DisplayTitle) &&
                                                             !string.IsNullOrWhiteSpace(x.Thumbnail));
            }
        }

        [RelayCommand]
        private void SelectMovie(Movie? selected = null)
        {
            if (selected is Movie movie)
            {
                if (SelectedMovie != selected)
                {
                    SelectedMovie = movie;
                    return;
                }
            }

            SelectedMovie = null;
        }


        [RelayCommand]
        private async Task Navigate(string destination = "")
        {
            string path = null;

            switch (destination.ToLower())
            {
                case "categories":
                    await Shell.Current.GoToAsync(nameof(CategoriesPage), true);
                    break;

                case "movies":
                    await Shell.Current.GoToAsync(nameof(MovieListPage), true, 
                                                  new Dictionary<string, object>() { { nameof(Genre), new Genre() { GenreType = GenreTypes.Movie } } });
                    break;

                case "tv":
                    await Shell.Current.GoToAsync(nameof(MovieListPage), true,
                                                  new Dictionary<string, object>() { { nameof(Genre), new Genre() { GenreType = GenreTypes.TV } } });
                    break;

                default: return;
            }
        }

        [RelayCommand]
        private async Task GoToDetails(Movie movie)
        {
            SelectedMovie = null;
            await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object>() { { nameof(Movie), movie } });
        }
    }
}
