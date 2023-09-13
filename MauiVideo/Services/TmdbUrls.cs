using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiVideo.Services
{
    public static class TmdbUrls
    {
        public const string BaseUrl = "https://api.themoviedb.org/3/";
        public const string LanguageParam = "language=en_US";
        public const string ApiKeyPath = "ApiKey.txt";

        public const string YoutubeUrl = "https://www.youtube.com/embed/{0}";
        public const string YoutubeThumbnailUrl = "https://img.youtube.com/vi/{0}/0.jpg";

        #region Images 

        public const string ThumbnailBaseUrl = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/{0}";
        public const string ThumbnailSmallBaseUrl = "https://image.tmdb.org/t/p/w220_and_h330_face/{0}";
        public const string Thumbnai1OriginallBaseUrl = "https://image.tmdb.org/t/p/original/{0}";

        #endregion
        public const string DiscoverAllMovies = $"discover/movie?include_adult=false&{LanguageParam}";
        public const string DiscoverAllTv = $"discover/tv?include_adult=false&{LanguageParam}";

        public const string Trending = $"trending/all/week?{LanguageParam}";
        public const string NetflixOriginals = $"discover/tv?{LanguageParam}&with_networks=213";
        public const string TopRated = $"movie/top_rated?{LanguageParam}";
        public const string Action = $"discover/movie?{LanguageParam}&with_genres=28";
        public const string MoviesGenre = $"genre/movie/list?{LanguageParam}";
        public const string TvGenre = $"genre/tv/list?{LanguageParam}";


        public static string GetMovies(string genreType = "movie", int page = 1) => $"discover/{genreType}?{LanguageParam}&page={page}";
        public static string GetMoviesByGenre(int genreId, string genreType = "movie", int page = 1) => $"discover/{genreType}?{LanguageParam}&with_genres={genreId}&page={page}";
        public static string GetTrailers(int movieId, string type = "movie") => $"{type}/{movieId}/videos?{LanguageParam}";
        public static string GetMovieDetails(int movieId, string type = "movie") => $"{type}/{movieId}?{LanguageParam}";
        public static string GetSimilar(int movieId, string type = "movie") => $"{type}/{movieId}/similar?{LanguageParam}";
    }
}
