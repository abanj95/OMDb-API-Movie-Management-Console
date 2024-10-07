using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MoviesClient.Models;
using Newtonsoft.Json;

namespace MoviesClient.Services
{
    public class MovieService
    {
        private readonly HttpClient _client;
        private readonly string _apiUrl = "http://localhost:5014/api/movies";

        public MovieService()
        {
            _client = new HttpClient();
        }

        // Get all movies
        public async Task<List<Movie>> GetAllMoviesAsync()
        {
            var response = await _client.GetAsync(_apiUrl);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Movie>>(responseBody);
        }

        // Add a new movie
        public async Task AddMovieAsync(Movie movie)
        {
            var content = new StringContent(JsonConvert.SerializeObject(movie), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_apiUrl, content);
            response.EnsureSuccessStatusCode();
        }

        // Update a movie
        public async Task UpdateMovieAsync(int id, Movie movie)
        {
            var content = new StringContent(JsonConvert.SerializeObject(movie), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{_apiUrl}/{id}", content);
            response.EnsureSuccessStatusCode();
        }

        // Delete a movie
        public async Task DeleteMovieAsync(int id)
        {
            var response = await _client.DeleteAsync($"{_apiUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
