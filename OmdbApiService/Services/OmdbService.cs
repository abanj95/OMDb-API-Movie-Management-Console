using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using OmdbApiService.Models;
using Microsoft.Extensions.Caching.Memory;
using OmdbApiService.Data;
using OmdbApiService.DTOs;

namespace OmdbApiService.Services
{
    public class OMDbService : IOMDbService
    {
        private readonly HttpClient _httpClient;
        //private readonly string _apiKey = "f3c35c0";
        private readonly string _apiKey;
        private readonly IMemoryCache _cache;
        private readonly MovieDbContext _dbContext;

        public OMDbService(HttpClient httpClient, IMemoryCache cache, MovieDbContext dbContext, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _cache = cache;
            _dbContext = dbContext;
            _apiKey = configuration["OMDbService:ApiKey"];
        }

        // Method to get a movie by title with caching and persistence
        public async Task<MovieDto> GetMovieByTitleAsync(string title)
        {
            // Check if the data is cached
            if (_cache.TryGetValue(title, out MovieDto cachedMovie))
            {
                return cachedMovie;
            }

            // Check if the data is persisted in MySQL
            var dbMovie = _dbContext.Movies.FirstOrDefault(m => m.Title == title);
            if (dbMovie != null)
            {
                // Add to cache and return data
                var movieDto = new MovieDto
                {
                    ImdbId = dbMovie.ImdbId,
                    Title = dbMovie.Title,
                    Year = dbMovie.Year,
                    // Map other fields from dbMovie as necessary
                };
                _cache.Set(title, movieDto, TimeSpan.FromMinutes(30));
                return movieDto;
            }

            // Fetch data from OMDb API if not found in cache or database
            var response = await _httpClient.GetAsync($"http://www.omdbapi.com/?t={title}&apikey={_apiKey}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var movie = JsonSerializer.Deserialize<MovieDto>(content);

            // Save data to cache
            _cache.Set(title, movie, TimeSpan.FromMinutes(30));

            // Save data to MySQL
            var dbEntity = new Movie
            {
                ImdbId = movie.ImdbId,
                Title = movie.Title,
                Year = movie.Year,
                // Map other fields as needed
            };
            _dbContext.Movies.Add(dbEntity);
            await _dbContext.SaveChangesAsync();

            return movie;
        }

        // Method to get a movie by IMDb ID with caching and persistence
        public async Task<MovieDto> GetMovieByIdAsync(string imdbId)
        {
            // Check if the data is cached
            if (_cache.TryGetValue(imdbId, out MovieDto cachedMovie))
            {
                return cachedMovie;
            }

            // Check if the data is persisted in MySQL
            var dbMovie = _dbContext.Movies.FirstOrDefault(m => m.ImdbId == imdbId);
            if (dbMovie != null)
            {
                // Add to cache and return data
                var movieDto = new MovieDto
                {
                    ImdbId = dbMovie.ImdbId,
                    Title = dbMovie.Title,
                    Year = dbMovie.Year,
                    // Map other fields from dbMovie as necessary
                };
                _cache.Set(imdbId, movieDto, TimeSpan.FromMinutes(30));
                return movieDto;
            }

            // Fetch data from OMDb API if not found in cache or database
            var response = await _httpClient.GetAsync($"http://www.omdbapi.com/?i={imdbId}&apikey={_apiKey}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var movie = JsonSerializer.Deserialize<MovieDto>(content);

            // Save data to cache
            _cache.Set(imdbId, movie, TimeSpan.FromMinutes(30));

            // Save data to MySQL
            var dbEntity = new Movie
            {
                ImdbId = movie.ImdbId,
                Title = movie.Title,
                Year = movie.Year,
                // Map other fields as needed
            };
            _dbContext.Movies.Add(dbEntity);
            await _dbContext.SaveChangesAsync();

            return movie;
        }

        // Method to get movies by year
        public async Task<List<MovieDto>> GetMoviesByYearAsync(string year)
        {
            // Check if the data is persisted in MySQL
            var dbMovies = _dbContext.Movies.Where(m => m.Year == year).ToList();

            if (dbMovies.Any())
            {
                return dbMovies.Select(dbMovie => new MovieDto
                {
                    ImdbId = dbMovie.ImdbId,
                    Title = dbMovie.Title,
                    Year = dbMovie.Year,
                    // Map other fields as necessary
                }).ToList();
            }

            // If not found, return an empty list
            return new List<MovieDto>();
        }

        // Method to get a movie by its database ID
        public async Task<MovieDto> GetMovieByDatabaseIdAsync(int id)
        {
            var dbMovie = await _dbContext.Movies.FindAsync(id);
            if (dbMovie == null) return null;

            return new MovieDto
            {
                ImdbId = dbMovie.ImdbId,
                Title = dbMovie.Title,
                Year = dbMovie.Year,
                // Map other fields as necessary
            };
        }
    }
}
