using System;
namespace OmdbApiService.Models
{
	public class Movie
	{
        public int Id { get; set; }
        public string? ImdbId { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
    }
}

