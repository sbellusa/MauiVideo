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
    [QueryProperty(nameof(Movie), nameof(Movie))]
    [QueryProperty(nameof(genreType), "type")]
    public partial class DetailsViewModel : ObservableObject
    {
        ITmdbService TmdbService { get; }

        Func<Video, bool> FilterTrailersVideos = v =>
        {
            return v.Official &&
            v.Site.Equals("youtube", StringComparison.OrdinalIgnoreCase) &&
            (v.Type.Equals("teaser", StringComparison.OrdinalIgnoreCase) || v.Type.Equals("trailer", StringComparison.OrdinalIgnoreCase));
        };

        public DetailsViewModel(ITmdbService tmdbService)
        {
            TmdbService = tmdbService;
        }

        string genreType = "movie";

        [ObservableProperty]
        Movie movie;

        [ObservableProperty]
        Video selectedTrailer;

        public ObservableCollection<Video> Videos { get; } = new();

        public ObservableCollection<Movie> SimilarMovies { get; } = new();

        [ObservableProperty]
        bool isBusy;

        [ObservableProperty]
        double similarItemsWidth;

        public async Task InitialiseAsync()
        {
            IsBusy = true;

            try
            {
                if (string.IsNullOrWhiteSpace(Movie?.MediaType)) { Movie.MediaType = genreType; }

                var trailersTask = TmdbService.GetTrailersAsync(Movie.Id, Movie.MediaType);
                var similarTask = TmdbService.GetSimilarMovies(Movie.Id, Movie.MediaType);

                await Task.WhenAll(trailersTask, similarTask);

                var trailers = trailersTask.Result;
                var similar = similarTask.Result;

                if (trailers.IsSuccessStatusCode && trailers.Results?.Count > 0)
                {
                    var youtubeVideos = trailers.Results?.Where(FilterTrailersVideos);

                    Videos.Clear();
                    foreach (var video in youtubeVideos) { Videos.Add(video); }

                    SelectedTrailer = youtubeVideos?.FirstOrDefault(x => x.Type.Equals("trailer", StringComparison.OrdinalIgnoreCase));

                    SelectedTrailer ??= youtubeVideos.FirstOrDefault();
                }
                if (similar.IsSuccessStatusCode && similar.Results?.Count > 0)
                {
                    SimilarMovies.Clear();
                    foreach (var movie in similar.Results)
                    {
                        if (!String.IsNullOrWhiteSpace(movie.ThumbnailPath))
                        {
                            SimilarMovies.Add(movie);
                        }
                    }
                }
            }
            catch
            {
            }

            IsBusy = false;
        }


        [RelayCommand]
        private async Task GoToDetails(Movie movie)
        {
            await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object>() { { nameof(Movie), movie } });
        }
    }
}
