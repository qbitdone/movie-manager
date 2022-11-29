using AutoMapper;
using Microsoft.EntityFrameworkCore;
using movie_manager.Data;
using movie_manager.Models;

namespace movie_manager.Services
{
    public class MovieService
    {
        private readonly MovieManagerDbContext _context;
        private readonly IMapper _mapper;

        public MovieService(MovieManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MovieResponse>> GetAllMovies() => await _context.Movies.Select(movie => _mapper.Map<MovieResponse>(movie)).ToListAsync();
        public async Task<MovieResponse> GetMovieById(Guid movieId)
        {
            var _movie = await _context.Movies.FirstOrDefaultAsync(movie => movie.Id == movieId);
            return _mapper.Map<MovieResponse>(_movie);
        }
        public async Task<bool> AddMovie(MovieRequest newMovie)
        {
            try
            {
                await _context.Movies.AddAsync(_mapper.Map<Movie>(newMovie));
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteMovieById(Guid movieId)
        {
            var _movie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == movieId);
            if (_movie != null)
            {
                _context.Movies.Remove(_movie);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Movie> UpdateMovieById(MovieRequest updatedMovie, Guid movieId)
        {
            var _movie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == movieId);

            if (_movie != null)
            {
                _movie.Title = updatedMovie.Title;
                _movie.Description = updatedMovie.Description;
                _movie.Duration = updatedMovie.Duration;
                _movie.Budget = updatedMovie.Budget;
                _movie.StartRecording = updatedMovie.StartRecording;
                _movie.EndRecording = updatedMovie.EndRecording;
                _movie.DirectorId = updatedMovie.DirectorId;

                await _context.SaveChangesAsync();
            }

            return _movie;
        }

        public async Task<List<MovieResponse>> GetAllDirectorMovies(Guid directorId)  
        {
            return await _context.Movies.Where(movie => movie.DirectorId == directorId).Select(movie => _mapper.Map<MovieResponse>(movie)).Distinct().ToListAsync();
        }


    }
}
