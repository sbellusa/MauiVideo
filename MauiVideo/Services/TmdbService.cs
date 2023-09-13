using MauiVideo.Http;
using MauiVideo.Models;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiVideo.Services
{
    public class TmdbService : ITmdbService
    {
        public const string TmdbHttpClientName = "TmdbClient";
        
        HttpClient httpClient;
        IHttpClientFactory _httpClientFactory;
        ISecretService _secretService;
        bool isInitialised = false;

        public TmdbService(IHttpClientFactory httpClientFactory, ISecretService secretService)
        {
            _httpClientFactory = httpClientFactory;
            _secretService = secretService;
        }

        /// <summary>
        /// Initialise the client with the ApiKey
        /// </summary>
        public async Task InitialiseHttpClient()
        {
            if (isInitialised) return;

            httpClient = _httpClientFactory.CreateClient(TmdbHttpClientName);
            
            string apiKey = await _secretService.RetrieveApiKey();

            httpClient.BaseAddress = new Uri(TmdbUrls.BaseUrl);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            isInitialised = true;
        }



        public async Task<MovieResponse> GetNetflixOriginalsAsync() => await GetMovies(TmdbUrls.NetflixOriginals);
        public async Task<MovieResponse> GetActionAsync() => await GetMovies(TmdbUrls.Action);
        public async Task<MovieResponse> GetTopRatedAsync() => await GetMovies(TmdbUrls.TopRated);
        public async Task<MovieResponse> GetTrendingAsync() => await GetMovies(TmdbUrls.Trending);
        public async Task<MovieResponse> GetAllMoviesAsync() => await GetMovies(TmdbUrls.DiscoverAllMovies);
        public async Task<MovieResponse> GetAllTvAsync() => await GetMovies(TmdbUrls.DiscoverAllTv);


        private async Task<MovieResponse> GetMovies(string url)
        {
            await InitialiseHttpClient();

            var request = new RequestBase<MovieResponse>(RequestMethods.Get, url);

            var response = await httpClient.ProcessAsync(request);

            return response;
        }

        public async Task<MovieResponse> GetMoviesByGenre(int genreId, GenreTypes genreType = GenreTypes.Movie, int page = 1)
        {
            await InitialiseHttpClient();

            var genreString = genreType.ToString().ToLowerInvariant();

            string url;

            if (genreId == 0)
            {
                url = TmdbUrls.GetMovies(genreString, page);
            }
            else
            {
                url = TmdbUrls.GetMoviesByGenre(genreId, genreString, page);
            }

            var request = new RequestBase<MovieResponse>(RequestMethods.Get, url);

            var response = await httpClient.ProcessAsync(request);

            return response;
        }

        public async Task<GenresResponse> GetGenresAsync(string type = "movie")
        {
            await InitialiseHttpClient();

            var url = String.Equals(type, "movie", StringComparison.InvariantCultureIgnoreCase) ?
                                TmdbUrls.MoviesGenre : TmdbUrls.TvGenre;

            var request = new RequestBase<GenresResponse>(RequestMethods.Get, url);

            var response = await httpClient.ProcessAsync(request);

            return response;
        }

        public async Task<VideoResponse> GetTrailersAsync(int id, string type = "movie")
        {
            await InitialiseHttpClient();

            var url = TmdbUrls.GetTrailers(id, type);

            var request = new RequestBase<VideoResponse>(RequestMethods.Get, url);

            var response = await httpClient.ProcessAsync(request);

            return response;
        }

        public async Task<MovieResponse> GetSimilarMovies(int id, string type = "movie")
        {
            await InitialiseHttpClient();

            var url = TmdbUrls.GetSimilar(id, type);

            var request = new RequestBase<MovieResponse>(RequestMethods.Get, url);

            var response = await httpClient.ProcessAsync(request);

            return response;
        }
    }
}
