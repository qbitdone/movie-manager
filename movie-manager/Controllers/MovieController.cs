using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using movie_manager.Models;
using movie_manager.Services;
using System.Data;

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
        [Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<MovieResponse>>> GetAllMovies() => Ok(await _movieService.GetAllMovies());

        [HttpGet("{movieId}")]
        [Authorize(Roles = "User, Director")]
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
        [Authorize(Roles = "User, Director")]
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
        [Authorize(Roles = "User, Director")]
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
        [Authorize(Roles = "User, Director")]
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
        [Authorize(Roles = "User, Director")]
        public async Task<ActionResult> GetAllDirectorMovies(Guid directorId) => Ok(await _movieService.GetAllDirectorMovies(directorId));

        [HttpPost("genre")]
        [Authorize(Roles = "User, Director")]
        public async Task<ActionResult> AddMovieGenres([FromBody] MovieGenreRequest newMovieGenre)
        {
            var isAdded = await _movieService.AddGenreToMovie(newMovieGenre.MovieId, newMovieGenre.GenreId);
            if (isAdded)
            {
                return Ok("You have successfully added new Genre to Movie!");
            }
            else
            {
                return BadRequest("Could not add new Genre to Movie - All fields are required");
            }
        }

        [HttpPost("invite")]
        [Authorize(Roles = "User, Director")]
        public async Task<ActionResult> InviteActorToMovie([FromBody] MovieActorRequest newMovieActor)
        {
            var isInvited = await _movieService.InviteActorToMovie(newMovieActor.MovieId, newMovieActor.ActorId);
            if (isInvited)
            {
                return Ok("You have successfully invited Actor to Movie!");
            }
            else
            {
                return BadRequest("Could not send invitation for Movie to Actor - All fields are required");
            }
        }

        [HttpPost("apply")]
        [Authorize(Roles = "User, Actor")]
        public async Task<ActionResult> SendApplicationForMovie([FromBody] MovieActorRequest newMovieActor)
        {
            var isSent = await _movieService.SendApplicationForMovie(newMovieActor.MovieId, newMovieActor.ActorId);
            if (isSent)
            {
                return Ok("You have successfully sent application for Movie!");
            }
            else
            {
                return BadRequest("Could not send application for Movie - All fields are required");
            }
        }
        

        [HttpGet("actor/{actorId}/invitations")]
        [Authorize(Roles = "User, Actor")]
        public async Task<ActionResult> GetAllActorInvitations(Guid actorId) => Ok(await _movieService.GetAllActorInvitations(actorId));

        [HttpGet("actor/{actorId}/applications")]
        [Authorize(Roles = "User, Actor")]
        public async Task<ActionResult> GetAllActorApplications(Guid actorId) => Ok(await _movieService.GetAllActorApplications(actorId));

        [HttpGet("actor/{actorId}/arrangement")]
        [Authorize(Roles = "User, Actor")]
        public async Task<ActionResult> GetAllActorArrangement(Guid actorId) => Ok(await _movieService.GetAllActorArrangement(actorId));
    }
}
