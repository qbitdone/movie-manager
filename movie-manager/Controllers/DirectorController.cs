using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using movie_manager.Models;
using movie_manager.Services;

namespace movie_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly DirectorService _directorService;

        public DirectorController(DirectorService directorService)
        {
            _directorService = directorService; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectorResponse>>> GetAllDirectors() => Ok(await _directorService.GetAllDirectors());

        [HttpPost]
        public async Task<ActionResult> AddDirector([FromBody] DirectorRequest newDirector)
        {
            var result = await _directorService.AddDirector(newDirector);
            if (result)
            {
                return Ok("You have successfully added new Director!");
            }
            else
            {
                return BadRequest("Could not add new Director - All fields are required");
            }
        }
        [HttpPut("{directorId}")]
        public async Task<ActionResult> UpdateDirectorById([FromBody] DirectorRequest newDirector, Guid directorId)
        {
            var updatedDirector = await _directorService.UpdateDirectorById(newDirector, directorId);
            if (updatedDirector == null)
            {
                return NotFound($"Director with id {directorId} does not exists"); 
            }
            return Ok($"You have successfully updated director with id {directorId}");
        }

        [HttpDelete("{directorId}")]
        public async Task<ActionResult> DeleteDirectorById(Guid directorId)
        {
            bool isDeleted = await _directorService.DeleteDirectorById(directorId); 
            if (!isDeleted)
            {
                return NotFound($"Director with id {directorId} does not exists"); 
            }
            return Ok($"You have successfully deleted director with id {directorId}"); 
        }
    }
}
