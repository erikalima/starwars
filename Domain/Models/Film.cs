using System;
using System.Text.Json.Serialization;

namespace StarWars.Api.Domain.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        [JsonPropertyName("release_date")]
        public DateTime ReleaseDate { get; set; }
    }
}