using Microsoft.AspNetCore.Mvc;
using OmdbApiService.Data;
using OmdbApiService.Models;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly MovieDbContext _context;

    public MoviesController(MovieDbContext context)
    {
        _context = context;
    }

    // GET: api/movies
    [HttpGet]
    public IActionResult GetMovies()
    {
        var movies = _context.Movies.ToList();
        return Ok(movies);
    }

    // GET: api/movies/{id}
    [HttpGet("{id}")]
    public IActionResult GetMovie(int id)
    {
        var movie = _context.Movies.Find(id);
        if (movie == null)
        {
            return NotFound();
        }
        return Ok(movie);
    }

    // POST: api/movies
    [HttpPost]
    public IActionResult AddMovie([FromBody] Movie movie)
    {
        _context.Movies.Add(movie);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
    }

    // PUT: api/movies/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateMovie(int id, [FromBody] Movie movie)
    {
        var existingMovie = _context.Movies.Find(id);
        if (existingMovie == null)
        {
            return NotFound();
        }
        existingMovie.Title = movie.Title;
        existingMovie.Year = movie.Year;
        // Update other properties as necessary

        _context.SaveChanges();
        return NoContent();
    }

    // DELETE: api/movies/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteMovie(int id)
    {
        var movie = _context.Movies.Find(id);
        if (movie == null)
        {
            return NotFound();
        }

        _context.Movies.Remove(movie);
        _context.SaveChanges();
        return NoContent();
    }
}
