using AutoMapper;
using Microsoft.EntityFrameworkCore;
using movie_manager.Data;
using movie_manager.Models;
using movie_manager.Pagination;
using System.Collections.Generic;

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

        public async Task<IEnumerable<MovieResponse>> GetAllMovies() => await _context.Movies
            .Include(movie => movie.Director)
            .Select(movie => _mapper.Map<MovieResponse>(movie))
            .ToListAsync();
        
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
            return await _context.Movies
                .Where(movie => movie.DirectorId == directorId)
                .Select(movie => _mapper.Map<MovieResponse>(movie)).Distinct().ToListAsync();
        }

        public async Task<bool> AddGenreToMovie(Guid movieId, Guid genreId)
        {
            try
            {
                await _context.MovieGenres.AddAsync(new MovieGenre
                {
                    MovieId = movieId,
                    GenreId = genreId
                });
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> InviteActorToMovie(Guid movieId, Guid actorId)
        {
            try
            {
                await _context.MovieActors.AddAsync(new MovieActor
                {
                    MovieId = movieId,
                    ActorId = actorId,
                    DirectorAccepted = true,
                    ActorAccepted = false,
                    
                });
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SendApplicationForMovie(Guid movieId, Guid actorId)
        {
            try
            {
                await _context.MovieActors.AddAsync(new MovieActor
                {
                    MovieId = movieId,
                    ActorId = actorId,
                    DirectorAccepted = false,
                    ActorAccepted = true,

                });
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<MovieResponse>> GetAllActorInvitations(Guid actorId) => await _context.MovieActors
                .Include(n => n.Movie)
                .Where(n => n.ActorId == actorId && n.DirectorAccepted == true && n.ActorAccepted == false)
                .Select(n => _mapper.Map<MovieResponse>(n.Movie))
                .ToListAsync();     
        

        public async Task<List<MovieResponse>> GetAllActorApplications(Guid actorId) => await _context.MovieActors
                .Include(n => n.Movie)
                .Where(n => n.ActorId == actorId && n.DirectorAccepted == false && n.ActorAccepted == true)
                .Select(n => _mapper.Map<MovieResponse>(n.Movie))
                .ToListAsync();

        public async Task<List<MovieResponse>> GetAllActorArrangement(Guid actorId) => await _context.MovieActors
                .Include(n => n.Movie)
                .Where(n => n.ActorId == actorId && n.DirectorAccepted == true && n.ActorAccepted == true)
                .Select(n => _mapper.Map<MovieResponse>(n.Movie))
                .ToListAsync();

        public async Task<List<ActorResponse>> GetAllMovieActors(Guid movieId) => await _context.MovieActors
            .Include(n => n.Movie)
            .Where(n => n.MovieId == movieId)
            .Select(n => _mapper.Map<ActorResponse>(n.Actor))
            .ToListAsync();

        public async Task<List<MovieResponse>> GetAllMoviesFiltered(Guid? genre, double? budget, DateTime? startOfMovie, DateTime? endOfMovie, int? pageNumber)
        {
            var allMovies = await GetAllMovies();

            // Filtering
            if (genre != null)
            {
                allMovies = await _context.MovieGenres
                .Include(n => n.Movie)
                .Where(n => n.GenreId == genre)
                .Select(n => _mapper.Map<MovieResponse>(n.Movie))
                .ToListAsync();
            }

            if (budget != null)
            {
                allMovies = await _context.Movies
                .Where(n => n.Budget <= budget)
                .Select(n => _mapper.Map<MovieResponse>(n))
                .ToListAsync();
            }

            if (startOfMovie != null)
            {
                allMovies = await _context.Movies
                .Where(n => n.StartRecording == startOfMovie)
                .Select(n => _mapper.Map<MovieResponse>(n))
                .ToListAsync();
            }

            if (endOfMovie != null)
            {
                allMovies = await _context.Movies
                .Where(n => n.EndRecording == endOfMovie)
                .Select(n => _mapper.Map<MovieResponse>(n))
                .ToListAsync();
            }

            //Paging
            int pageSize = 5;
            allMovies = PaginatedList<MovieResponse>.Create(allMovies.AsQueryable(), pageNumber ?? 1, pageSize);
            return (List<MovieResponse>)allMovies;
        } 
    }
}
