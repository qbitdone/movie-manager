using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using movie_manager.Models;
using movie_manager.Services;

namespace movie_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public UserController(UserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers() => Ok(await _userService.GetAllUsers());


        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody] UserRequest newUser)
        {
            var result = await _userService.AddUser(_mapper.Map<User>(newUser));
            if (result)
            {
                return Ok("You have successfully added new User!");
            } else
            {
                return BadRequest("Could not add new User - All fields are required");
            }
        }
    }
}
