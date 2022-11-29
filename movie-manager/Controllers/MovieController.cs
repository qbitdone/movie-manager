using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using movie_manager.Models;
using movie_manager.Services;

namespace movie_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieService _movieService;

        public MovieController(MovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieResponse>>> GetAllMovies() => Ok(await _movieService.GetAllMovies());

        [HttpGet("{movieId}")]
        public async Task<ActionResult<IEnumerable<MovieResponse>>> GetMovieById(Guid movieId)
        {
            var _movie = await _movieService.GetMovieById(movieId);
            if (_movie != null)
            {
                return Ok(await _movieService.GetMovieById(movieId));

            }
            return NotFound($"Movie with id {movieId} does not exists");
        }
        [HttpPost]
        public async Task<ActionResult> AddMovie([FromBody] MovieRequest newMovie)
        {
            var isAdded = await _movieService.AddMovie(newMovie);
            if (isAdded)
            {
                return Ok("You have successfully added new Movie!");
            }
            else
            {
                return BadRequest("Could not add new Movie - All fields are required");
            }
        }

        [HttpPut("{movieId}")]
        public async Task<ActionResult> UpdateMovieById([FromBody] MovieRequest newMovie, Guid movieId)
        {
            var updatedMovie = await _movieService.UpdateMovieById(newMovie, movieId);
            if (updatedMovie == null)
            {
                return NotFound($"Movie with id {movieId} does not exists");
            }
            return Ok($"You have successfully updated movie with id {movieId}");
        }

        [HttpDelete("{movieId}")]
        public async Task<ActionResult> DeleteMovieById(Guid movieId)
        {
            bool isDeleted = await _movieService.DeleteMovieById(movieId);
            if (!isDeleted)
            {
                return NotFound($"Movie with id {movieId} does not exists");
            }
            return Ok($"You have successfully deleted movie with id {movieId}");
        }
        [HttpGet("director/{directorId}")]
        public async Task<ActionResult> GetAllDirectorMovies(Guid directorId) => Ok(await _movieService.GetAllDirectorMovies(directorId));
    }
}
