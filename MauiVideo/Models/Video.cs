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
    public class Video
    {
        [JsonPropertyName("iso_639_1")]
        public string Iso6391 { get; set; }

        [JsonPropertyName("iso_3166_1")]
        public string Iso31661 { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("site")]
        public string Site { get; set; }

        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("official")]
        public bool Official { get; set; }

        [JsonPropertyName("published_at")]
        public DateTime PublishedAt { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }



        [JsonIgnore]
        public string VideoUrl => String.Format(TmdbUrls.YoutubeUrl, Key);
        [JsonIgnore]
        public string ThumbnailUrl => String.Format(TmdbUrls.YoutubeThumbnailUrl, Key);
    }

    public class VideoResponse : ResponseBase
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("results")]
        public List<Video> Results { get; set; }
    }
}
