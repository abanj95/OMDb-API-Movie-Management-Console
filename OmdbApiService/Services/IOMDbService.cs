using System.Collections.Generic;
using System.Threading.Tasks;
using OmdbApiService.DTOs;

namespace OmdbApiService.Services
{
    public interface IOMDbService
    {
        // Search a movie by title
        Task<MovieDto> GetMovieByTitleAsync(string title);

        // Search a movie by IMDb ID
        Task<MovieDto> GetMovieByIdAsync(string imdbId);

        // Search movies by year
        Task<List<MovieDto>> GetMoviesByYearAsync(string year);

        // Search a movie by its ID (in the database)
        Task<MovieDto> GetMovieByDatabaseIdAsync(int id);
    }
}
