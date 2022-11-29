using AutoMapper;
using Microsoft.EntityFrameworkCore;
using movie_manager.Data;
using movie_manager.Models;

namespace movie_manager.Services
{
    public class GenreService
    {
        private readonly MovieManagerDbContext _context;
        private readonly IMapper _mapper;

        public GenreService(MovieManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GenreResponse>> GetAllGenres() => await _context.Genres.Select(genre => _mapper.Map<GenreResponse>(genre)).ToListAsync();

        public async Task<bool> AddGenre(GenreRequest newGenre)
        {
            try
            {
                await _context.Genres.AddAsync(_mapper.Map<Genre>(newGenre));
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteGenreById(Guid genreId)
        {
            var _genre = await _context.Genres.FirstOrDefaultAsync(n => n.Id == genreId);
            if (_genre != null)
            {
                _context.Genres.Remove(_genre);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Genre> UpdateGenreById(GenreRequest updatedGenre, Guid genreId)
        {
            var _genre = await _context.Genres.FirstOrDefaultAsync(n => n.Id == genreId);

            if (_genre != null)
            {
                _genre.Name = updatedGenre.Name;

                await _context.SaveChangesAsync();
            }

            return _genre;
        }
    }
}
