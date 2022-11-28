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

        public DirectorService(MovieManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DirectorResponse>> GetAllDirectors() => await _context.Directors.Select(director => _mapper.Map<DirectorResponse>(director)).ToListAsync();

        public async Task<bool> AddDirector(DirectorRequest newDirector)
        {
            try
            {
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

    }
}
