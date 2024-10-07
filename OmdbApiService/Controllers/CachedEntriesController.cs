using Microsoft.AspNetCore.Mvc;
using OmdbApiService.Data;
using OmdbApiService.Models;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace OmdbApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CachedEntriesController : ControllerBase
    {
        private readonly MovieDbContext _dbContext;

        public CachedEntriesController(MovieDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // OData GET call to search all movies (with filtering, sorting, etc.)
        [HttpGet]
        [EnableQuery]
        public IActionResult GetMovies()
        {
            return Ok(_dbContext.Movies.AsQueryable());
        }

        // GET: Retrieve a single movie by its ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _dbContext.Movies.FindAsync(id);
            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        // New GET: Retrieve movies by title
        [HttpGet("title/{title}")]
        public async Task<IActionResult> GetMoviesByTitle(string title)
        {
            var movies = await _dbContext.Movies
                .Where(m => m.Title.Contains(title))
                .ToListAsync();

            if (!movies.Any())
                return NotFound("No movies found with that title.");

            return Ok(movies);
        }

        // New GET: Retrieve movies by year
        [HttpGet("year/{year}")]
        public async Task<IActionResult> GetMoviesByYear(string year)
        {
            var movies = await _dbContext.Movies
                .Where(m => m.Year == year)
                .ToListAsync();

            if (!movies.Any())
                return NotFound("No movies found from that year.");

            return Ok(movies);
        }

        // New GET: Retrieve a single movie by IMDb ID
        [HttpGet("imdb/{imdbId}")]
        public async Task<IActionResult> GetMovieByImdbId(string imdbId)
        {
            var movie = await _dbContext.Movies
                .FirstOrDefaultAsync(m => m.ImdbId == imdbId);

            if (movie == null)
                return NotFound("No movie found with that IMDb ID.");

            return Ok(movie);
        }

        // POST: Create a new movie entry
        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromBody] Movie movie)
        {
            if (movie == null)
                return BadRequest();

            await _dbContext.Movies.AddAsync(movie);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        // PUT: Update an existing movie entry
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] Movie updatedMovie)
        {
            if (id != updatedMovie.Id)
                return BadRequest("Movie ID mismatch");

            var movie = await _dbContext.Movies.FindAsync(id);
            if (movie == null)
                return NotFound();

            // Update properties
            movie.Title = updatedMovie.Title;
            movie.Year = updatedMovie.Year;
            // Add other properties as necessary

            _dbContext.Entry(movie).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Error updating the movie.");
            }

            return NoContent();
        }

        // DELETE: Delete a movie entry by its ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _dbContext.Movies.FindAsync(id);
            if (movie == null)
                return NotFound();

            _dbContext.Movies.Remove(movie);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
