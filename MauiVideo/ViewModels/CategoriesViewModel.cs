using CommunityToolkit.Maui.Core.Extensions;
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
    public partial class CategoriesViewModel : ObservableObject
    {
        private ITmdbService TmdbService { get; }
        bool hasInitialised = false;

        public CategoriesViewModel(ITmdbService tmdbService)
        {
            TmdbService = tmdbService;
        }

        [ObservableProperty]
        List<Genre> movieGenres;

        [ObservableProperty]
        List<Genre> tvGenres;

        public async Task InitialiseAsync()
        {
            if (hasInitialised) return;

            var movieResultTask = TmdbService.GetGenresAsync("movie");
            var tvResultTask = TmdbService.GetGenresAsync("tv");

            await Task.WhenAll(movieResultTask, tvResultTask);

            var movieResult = movieResultTask.IsCompletedSuccessfully ? movieResultTask.Result : null;
            var tvResult = tvResultTask.IsCompletedSuccessfully ? tvResultTask.Result : null;

            var mergedList = new List<Genre>();

            if (movieResult != null && movieResult.IsSuccessStatusCode)
            {
                if (movieResult.Genres?.Count > 0)
                {
                    movieResult.Genres.ForEach(x => x.GenreType = GenreTypes.Movie);
                    MovieGenres = new List<Genre>(movieResult.Genres);
                }
            }

            if (tvResult != null && tvResult.IsSuccessStatusCode)
            {
                if (tvResult.Genres?.Count > 0)
                {
                    tvResult.Genres.ForEach(x => x.GenreType = GenreTypes.TV);
                    TvGenres = new List<Genre>(tvResult.Genres);
                }
            }

            hasInitialised = true;
        }

        [RelayCommand]
        private async Task NavigateToCategory(Genre selected)
        {
            await Shell.Current.GoToAsync(nameof(MovieListPage), true,
                new Dictionary<string, object>() {
                    { nameof(Genre), selected },
                    { "type", selected.GenreType }
                });
        }
    }
}
