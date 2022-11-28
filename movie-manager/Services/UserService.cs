using AutoMapper;
using Microsoft.EntityFrameworkCore;
using movie_manager.Data;
using movie_manager.Models;

namespace movie_manager.Services
{
    public class UserService
    {
        private readonly MovieManagerDbContext _context;
        private readonly IMapper _mapper;

        public UserService(MovieManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsers() => await _context.Users.Select(user => _mapper.Map<UserResponse>(user)).ToListAsync();
        public async Task<bool> AddUser(UserRequest newUser)
        {
            try
            {
                await _context.Users.AddAsync(_mapper.Map<User>(newUser));
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
