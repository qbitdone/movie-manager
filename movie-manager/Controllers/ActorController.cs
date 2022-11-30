using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using movie_manager.Models;
using movie_manager.Services;

namespace movie_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly ActorService _actorService;

        public ActorController(ActorService actorService)
        {
            _actorService = actorService;   
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<ActorResponse>>> GetAllActors() => Ok(await _actorService.GetAllActors());

        [HttpGet("{actorId}")]
        [Authorize(Roles = "User, Actor")]
        public async Task<ActionResult<IEnumerable<ActorResponse>>> GetActorById(Guid actorId)
        {
            var _actor = await _actorService.GetActorById(actorId);
            if (_actor != null)
            {
                return Ok(_actor);

            }
            return NotFound($"Actor with id {actorId} does not exists");
        }
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> AddActor([FromBody] ActorRequest newActor)
        {
            var isAdded = await _actorService.AddActor(newActor);
            if (isAdded)
            {
                return Ok("You have successfully added new Actor!");
            }
            else
            {
                return BadRequest("Could not add new Actor - All fields are required/Username already exists");
            }
        }

        [HttpPut("{actorId}")]
        [Authorize(Roles = "User, Actor")]
        public async Task<ActionResult> UpdateActorById([FromBody] ActorRequest newActor, Guid actorId)
        {
            var _updatedActor = await _actorService.UpdateActorById(newActor, actorId);
            if (_updatedActor == null)
            {
                return NotFound($"Actor with id {actorId} does not exists");
            }
            return Ok($"You have successfully updated actor with id {actorId}");
        }

        [HttpDelete("{actorId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> DeleteActorById(Guid actorId)
        {
            bool isDeleted = await _actorService.DeleteActorById(actorId);
            if (!isDeleted)
            {
                return NotFound($"Actor with id {actorId} does not exists");
            }
            return Ok($"You have successfully deleted actor with id {actorId}");
        }
    }
}
