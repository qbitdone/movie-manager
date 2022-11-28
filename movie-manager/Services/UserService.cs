using movie_manager.Data;

namespace movie_manager.Services
{
    public class UserService
    {
        private readonly MovieManagerDbContext _context;

        public UserService(MovieManagerDbContext context)
        {
            _context = context; 
        }
    }
}
