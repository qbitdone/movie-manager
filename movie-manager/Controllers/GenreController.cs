using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using movie_manager.Models;
using movie_manager.Services;

namespace movie_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly GenreService _genreService;

        public GenreController(GenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<GenreResponse>>> GetAllGenres() => Ok(await _genreService.GetAllGenres());

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> AddGenre([FromBody] GenreRequest newGenre)
        {
            var result = await _genreService.AddGenre(newGenre);
            if (result)
            {
                return Ok("You have successfully added new Genre!");
            }
            else
            {
                return BadRequest("Could not add new Genre - All fields are required");
            }
        }

        [HttpPut("{genreId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> UpdateGenreById([FromBody] GenreRequest newGenre, Guid genreId)
        {
            var updatedGenre = await _genreService.UpdateGenreById(newGenre, genreId);
            if (updatedGenre == null)
            {
                return NotFound($"Genre with id {genreId} does not exists");
            }
            return Ok($"You have successfully updated genre with id {genreId}");
        }

        [HttpDelete("{genreId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> DeleteGenreById(Guid genreId)
        {
            bool isDeleted = await _genreService.DeleteGenreById(genreId);
            if (!isDeleted)
            {
                return NotFound($"Genre with id {genreId} does not exists");
            }
            return Ok($"You have successfully deleted genre with id {genreId}");
        }
    }
}
