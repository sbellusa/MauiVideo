using MauiVideo.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MauiVideo.Models
{
    public enum GenreTypes { Movie, TV }

    public class Genre
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonIgnore]
        public GenreTypes GenreType { get; set; }
    }

    public class GenresResponse : ResponseBase
    {
        [JsonPropertyName("genres")]
        public List<Genre> Genres { get; set; }
    }

    public class GroupedGenres : List<Genre>
    {
        public string Name { get; set; }
        public GroupedGenres(string name, List<Genre> list) : base(list)
        {
            Name = name;
        }
    }
}
