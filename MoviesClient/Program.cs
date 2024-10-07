using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MoviesConsoleClient
{
    class Program
    {
        private static readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5014/api/CachedEntries/") };

        static async Task Main(string[] args)
        {
            Console.WriteLine("OMDb API Client Console App");

            while (true)
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. Search movie by title");
                Console.WriteLine("2. Search movie by year");
                Console.WriteLine("3. Search movie by ID");
                Console.WriteLine("4. Create a custom movie entry");
                Console.WriteLine("5. Update a cached entry");
                Console.WriteLine("6. Delete a cached entry");
                Console.WriteLine("7. Query all cached entries");
                Console.WriteLine("8. Exit");
                Console.Write("Choice: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await SearchMovieByTitle();
                        break;
                    case "2":
                        await SearchMovieByYear();
                        break;
                    case "3":
                        await SearchMovieById();
                        break;
                    case "4":
                        await CreateCustomEntry();
                        break;
                    case "5":
                        await UpdateMovie();
                        break;
                    case "6":
                        await DeleteMovie();
                        break;
                    case "7":
                        await DisplayCachedEntries();
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Invalid choice, try again.");
                        break;
                }
            }
        }

        //static async Task SearchMovieByTitle()
        //{
        //    Console.Write("Enter movie title: ");
        //    var title = Console.ReadLine();
        //    var response = await _httpClient.GetAsync($"title/{title}");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var movie = await response.Content.ReadFromJsonAsync<MovieDto>();
        //        DisplayMovie(movie);
        //    }
        //    else
        //    {
        //        Console.WriteLine("Movie not found or an error occurred.");
        //    }
        //}
        static async Task SearchMovieByTitle()
        {
            Console.Write("Enter movie title: ");
            var title = Console.ReadLine();
            var response = await _httpClient.GetAsync($"title/{title}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                // Define deserialization options
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                // Deserialize with options
                var movie = JsonSerializer.Deserialize<MovieDto>(content, options);

                DisplayMovie(movie);
            }
            else
            {
                Console.WriteLine("Movie not found or an error occurred.");
            }
        }

        static async Task SearchMovieByYear()
        {
            Console.Write("Enter movie year: ");
            var year = Console.ReadLine();
            var response = await _httpClient.GetAsync($"year/{year}");

            if (response.IsSuccessStatusCode)
            {
                var movies = await response.Content.ReadFromJsonAsync<List<MovieDto>>();
                foreach (var movie in movies)
                {
                    DisplayMovie(movie);
                }
            }
            else
            {
                Console.WriteLine("Movies not found or an error occurred.");
            }
        }

        static async Task SearchMovieById()
        {
            Console.Write("Enter ID: ");
            var id = Console.ReadLine();
            var response = await _httpClient.GetAsync($"id/{id}");

            if (response.IsSuccessStatusCode)
            {
                var movie = await response.Content.ReadFromJsonAsync<MovieDto>();
                DisplayMovie(movie);
            }
            else
            {
                Console.WriteLine("Movie not found or an error occurred.");
            }
        }

        static async Task CreateCustomEntry()
        {
            Console.Write("Enter movie title: ");
            var title = Console.ReadLine();
            Console.Write("Enter movie year: ");
            var year = Console.ReadLine();

            var movie = new MovieDto { Title = title, Year = year };
            var response = await _httpClient.PostAsJsonAsync("", movie);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Movie entry created successfully.");
            }
            else
            {
                Console.WriteLine("An error occurred while creating the movie.");
            }
        }

        static async Task UpdateMovie()
        {
            Console.Write("Enter movie ID to update: ");
            var id = int.Parse(Console.ReadLine());
            Console.Write("Enter new title: ");
            var title = Console.ReadLine();
            Console.Write("Enter new year: ");
            var year = Console.ReadLine();

            var movie = new MovieDto { Title = title, Year = year };
            var response = await _httpClient.PutAsJsonAsync($"{id}", movie);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Movie updated successfully.");
            }
            else
            {
                Console.WriteLine("An error occurred while updating the movie.");
            }
        }

        static async Task DeleteMovie()
        {
            Console.Write("Enter movie ID to delete: ");
            var id = int.Parse(Console.ReadLine());

            var response = await _httpClient.DeleteAsync($"{id}");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Movie deleted successfully.");
            }
            else
            {
                Console.WriteLine("An error occurred while deleting the movie.");
            }
        }

        static async Task DisplayCachedEntries()
        {
            var response = await _httpClient.GetAsync("");

            if (response.IsSuccessStatusCode)
            {
                var movies = await response.Content.ReadFromJsonAsync<List<MovieDto>>();
                foreach (var movie in movies)
                {
                    DisplayMovie(movie);
                }
            }
            else
            {
                Console.WriteLine("No cached entries found or an error occurred.");
            }
        }

        static void DisplayMovie(MovieDto movie)
        {
            Console.WriteLine("=================================");
            Console.WriteLine($"ID: {movie.Id}");
            Console.WriteLine($"Title: {movie.Title}");
            Console.WriteLine($"Year: {movie.Year}");
            Console.WriteLine($"IMDb ID: {movie.ImdbId}"); // Corrected name
            // Display other properties as needed
            Console.WriteLine("=================================");
        }
    }

    public class MovieDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Year { get; set; }

        [JsonPropertyName("imdbID")]
        public string ImdbId { get; set; } // Corrected name and JSON property name
    }
}
