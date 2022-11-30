using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using movie_manager.Data;
using movie_manager.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace movie_manager.Services
{
    public class UserService
    {
        private readonly MovieManagerDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(MovieManagerDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsers() => await _context.Users.Select(user => _mapper.Map<UserResponse>(user)).ToListAsync();
        public async Task<bool> AddUser(UserRequest newUser)
        {
            try
            {
                if (await CheckIfUserExists(newUser.Username))
                {
                    return false;
                }
                await _context.Users.AddAsync(_mapper.Map<User>(newUser));
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> Authenticate(UserLogin userLogin)
        {
            if (!string.IsNullOrEmpty(userLogin.Username) && !string.IsNullOrEmpty(userLogin.Password))
            {
                var user = await _context.Users.FirstOrDefaultAsync(n => n.Username.Equals(userLogin.Username)
                && n.Password.Equals(userLogin.Password));

                if (user is not null)
                {
                    return user.Role;
                }

                var director = await _context.Directors.FirstOrDefaultAsync(n => n.Username.Equals(userLogin.Username)
                && n.Password.Equals(userLogin.Password));

                if (director is not null)
                {
                    return director.Role;
                }

                var actor = await _context.Actors.FirstOrDefaultAsync(n => n.Username.Equals(userLogin.Username)
                && n.Password.Equals(userLogin.Password));

                if (actor is not null)
                {
                    return actor.Role;
                }
            }
            return null;
        }

        public async Task<string> GenerateToken(string role)
        {
            var claims = new[]
                {
                    new Claim(ClaimTypes.Role, role),
                };

            var token = new JwtSecurityToken
            (
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])), SecurityAlgorithms.HmacSha256)
            );

             return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> Login(UserLogin userLogin)
        {
            var userRole = await Authenticate(userLogin);

            if (userRole != null)
            {
                var token = await GenerateToken(userRole);
                return token;
            }
            return null;
        }

        public async Task<bool> CheckIfUserExists(string username) => (
            await _context.Users.AnyAsync(u => u.Username.Equals(username)) ||
            await _context.Directors.AnyAsync(d => d.Username.Equals(username)) || 
            await _context.Actors.AnyAsync(a => a.Username.Equals(username))
            );

    }
}
