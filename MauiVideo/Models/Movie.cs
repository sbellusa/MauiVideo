using MauiVideo.Http;
using MauiVideo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MauiVideo.Models
{
    public class MovieResponse : ResponseBase
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("results")]
        public List<Movie> Results { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("total_results")]
        public int TotalResults { get; set; }
    }

    public class Movie
    {
        [JsonPropertyName("adult")]
        public bool Adult { get; set; }

        [JsonPropertyName("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("original_language")]
        public string OriginalLanguage { get; set; }

        [JsonPropertyName("original_title")]
        public string OriginalTitle { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }

        [JsonPropertyName("media_type")]
        public string MediaType { get; set; } // movie or tv

        [JsonPropertyName("genre_ids")]
        public List<int> GenreIds { get; set; }

        [JsonPropertyName("popularity")]
        public double Popularity { get; set; }

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }

        [JsonPropertyName("video")]
        public bool Video { get; set; }

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }

        [JsonPropertyName("vote_count")]
        public int VoteCount { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("original_name")]
        public string OriginalName { get; set; }

        [JsonPropertyName("first_air_date")]
        public string FirstAirDate { get; set; }

        [JsonPropertyName("origin_country")]
        public List<string> OriginCountry { get; set; }



        [JsonIgnore]
        public string ThumbnailPath => PosterPath ?? BackdropPath;
        [JsonIgnore]
        public string Thumbnail => string.Format(TmdbUrls.ThumbnailBaseUrl, ThumbnailPath);
        [JsonIgnore]
        public string ThumbnailSmall => string.Format(TmdbUrls.ThumbnailSmallBaseUrl, ThumbnailPath);
        [JsonIgnore]
        public string ThumbnailUrl => string.Format(TmdbUrls.Thumbnai1OriginallBaseUrl, ThumbnailPath);
        [JsonIgnore]
        public string BackdropUrl => string.Format(TmdbUrls.Thumbnai1OriginallBaseUrl, BackdropPath);
        [JsonIgnore]
        public string BackdropSmallUrl => string.Format(TmdbUrls.ThumbnailSmallBaseUrl, BackdropPath);
        [JsonIgnore]
        public string PosterUrl => string.Format(TmdbUrls.Thumbnai1OriginallBaseUrl, PosterPath);


        [JsonIgnore]
        public string DisplayTitle => Title ?? Name ?? OriginalTitle ?? OriginalName;
    }
}
