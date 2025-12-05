using System.Collections.Generic;

namespace OmdbApi.Models
{
    public class OmdbSearchResult
    {
        public List<OmdbMovie>? Search { get; set; }
        public string? TotalResults { get; set; }
        public string? Response { get; set; }
    }

    public class OmdbMovie
    {
        public string? Title { get; set; }
        public string? Year { get; set; }
        public string? ImdbID { get; set; }
        public string? Type { get; set; }
        public string? Poster { get; set; }
    }
}