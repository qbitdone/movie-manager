using Microsoft.EntityFrameworkCore;
using movie_manager.Data;
using movie_manager.Models;

namespace movie_manager.Services
{
    public class UserService
    {
        private readonly MovieManagerDbContext _context;

        public UserService(MovieManagerDbContext context)
        {
            _context = context; 
        }

        public async Task<IEnumerable<User>> GetAllUsers() => await _context.Users.ToListAsync();
       
    }
}
