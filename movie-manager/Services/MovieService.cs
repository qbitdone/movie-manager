using AutoMapper;
using Microsoft.EntityFrameworkCore;
using movie_manager.Data;
using movie_manager.Models;
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

        public async Task<List<MovieResponse>> GetAllActorInvitations(Guid actorId)
        {
            var _actorInvitations = await _context.MovieActors
                .Where(x => x.ActorId == actorId && x.DirectorAccepted == true && x.ActorAccepted == false)
                .ToListAsync();

            List<MovieResponse> _actorInvitedMovies = new List<MovieResponse>();
            foreach (var actorInvitation in _actorInvitations)
            {
                foreach (var movie in _context.Movies)
                {
                    if (movie.Id.Equals(actorInvitation.MovieId))
                    {
                        _actorInvitedMovies.Add(_mapper.Map<MovieResponse>(movie));
                    }
                }
            }
            return _actorInvitedMovies;
                
        }

        public async Task<List<MovieResponse>> GetAllActorApplications(Guid actorId)
        {
            var _actorApplications = await _context.MovieActors
                .Where(x => x.ActorId == actorId && x.DirectorAccepted == false && x.ActorAccepted == true)
                .ToListAsync();

            List<MovieResponse> _actorAppliedMovies = new List<MovieResponse>();
            foreach (var actorApplication in _actorApplications)
            {
                foreach (var movie in _context.Movies)
                {
                    if (movie.Id.Equals(actorApplication.MovieId))
                    {
                        _actorAppliedMovies.Add(_mapper.Map<MovieResponse>(movie));
                    }
                }
            }
            return _actorAppliedMovies;

        }
    }
}
