using MauiVideo.Http;
using MauiVideo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiVideo.Services
{
    public interface ITmdbService
    {
        Task<MovieResponse> GetTrendingAsync();
        Task<MovieResponse> GetNetflixOriginalsAsync();
        Task<MovieResponse> GetActionAsync();
        Task<MovieResponse> GetTopRatedAsync();


        Task<GenresResponse> GetGenresAsync(string type = "movie");

        Task<VideoResponse> GetTrailersAsync(int id, string type = "movie");

        Task<MovieResponse> GetMoviesByGenre(int genreId, GenreTypes genreType = GenreTypes.Movie, int page = 1);

        Task<MovieResponse> GetSimilarMovies(int id, string type = "movie");
    }
}
