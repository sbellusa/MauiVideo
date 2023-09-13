using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiVideo.Models;
using MauiVideo.Services;
using MauiVideo.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiVideo.ViewModels
{
    [QueryProperty(nameof(Genre), nameof(Genre))]
    public partial class MovieListViewModel : ObservableObject
    {
        ITmdbService TmdbService { get; }

        public MovieListViewModel(ITmdbService tmdbService)
        {
            TmdbService = tmdbService;
        }

        public ObservableCollection<Movie> Movies { get; } = new();

        [ObservableProperty]
        Genre genre;

        [ObservableProperty]
        bool isBusy;

        int page = 0;

        public async Task InitialiseAsync()
        {
            await LoadMore();
        }

        [RelayCommand]
        private async Task LoadMore()
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {
                var moviesResponse = await TmdbService.GetMoviesByGenre(Genre.Id, Genre.GenreType, page + 1);

                if (moviesResponse != null && moviesResponse.IsSuccessStatusCode)
                {
                    page = moviesResponse.Page;

                    foreach (var movie in moviesResponse.Results)
                    {
                        Movies.Add(movie);
                    }
                }
            }
            catch { }

            IsBusy = false;
        }


        [RelayCommand]
        private async Task GoToDetails(Movie movie)
        {
            await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object>() { { nameof(Movie), movie } });
        }
    }
}
