using AutoMapper;
using Microsoft.EntityFrameworkCore;
using movie_manager.Data;
using movie_manager.Models;

namespace movie_manager.Services
{
    public class DirectorService
    {
        private readonly MovieManagerDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public DirectorService(MovieManagerDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<IEnumerable<DirectorResponse>> GetAllDirectors() => await _context.Directors
            .Include(director => director.Movies)
            .Select(director => _mapper.Map<DirectorResponse>(director))
            .ToListAsync();

        public async Task<bool> AddDirector(DirectorRequest newDirector)
        {
            try
            {
                if (await _userService.CheckIfUserExists(newDirector.Username))
                {
                    return false;
                }
                await _context.Directors.AddAsync(_mapper.Map<Director>(newDirector));
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteDirectorById(Guid directorId)
        {
            var _director = await _context.Directors.FirstOrDefaultAsync(n => n.Id == directorId);
            if (_director != null)
            {
                _context.Directors.Remove(_director);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Director> UpdateDirectorById(DirectorRequest updatedDirector, Guid directorId)
        {
            var _director = await _context.Directors.FirstOrDefaultAsync(n => n.Id == directorId);

            if (_director != null)
            {
                _director.Name = updatedDirector.Name;
                _director.Surname = updatedDirector.Surname;
                _director.Username = updatedDirector.Username;
                _director.Password = updatedDirector.Password;

                await _context.SaveChangesAsync();
            }

            return _director;
        }

    }
}
