using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
                
        
        public UserController(UserService userService, IMapper mapper)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllUsers()
            => Ok(await _userService.GetAllUsers());


        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody] UserRequest newUser)
        {
            var result = await _userService.AddUser(newUser);
            if (result)
            {
                return Ok("You have successfully added new User!");
            } else
            {
                return BadRequest("Could not add new User - All fields are required/Username already exists");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserLogin userLogin)
        {
            var token = await _userService.Login(userLogin);
            if (token != null)
            {
                return Ok(token);
            }
            return NotFound("Login failed - Check your credentials");
        }
    }
}
