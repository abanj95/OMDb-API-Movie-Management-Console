using Microsoft.AspNetCore.Mvc;
using OmdbApiService.Services;
using System.Threading.Tasks;

namespace OmdbApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OMDbController : ControllerBase
    {
        private readonly IOMDbService _omdbService;

        public OMDbController(IOMDbService omdbService)
        {
            _omdbService = omdbService;
        }

        [HttpGet("title/{title}")]
        public async Task<IActionResult> GetMovieByTitle(string title)
        {
            var movie = await _omdbService.GetMovieByTitleAsync(title);
            return Ok(movie);
        }

        [HttpGet("id/{imdbId}")]
        public async Task<IActionResult> GetMovieById(string imdbId)
        {
            var movie = await _omdbService.GetMovieByIdAsync(imdbId);
            return Ok(movie);
        }
    }
}
