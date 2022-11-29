using AutoMapper;
using Microsoft.EntityFrameworkCore;
using movie_manager.Data;
using movie_manager.Models;

namespace movie_manager.Services
{
    public class ActorService
    {
        private readonly MovieManagerDbContext _context;
        private readonly IMapper _mapper;

        public ActorService(MovieManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ActorResponse>> GetAllActors() => await _context.Actors.Select(actor => _mapper.Map<ActorResponse>(actor)).ToListAsync();
        public async Task<ActorResponse> GetActorById(Guid actorId)
        {
            var _actor = await _context.Actors.FirstOrDefaultAsync(actor => actor.Id == actorId);
            return _mapper.Map<ActorResponse>(_actor);
        }
        public async Task<bool> AddActor(ActorRequest newActor)
        {
            try
            {
                await _context.Actors.AddAsync(_mapper.Map<Actor>(newActor));
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteActorById(Guid actorId)
        {
            var _actor = await _context.Actors.FirstOrDefaultAsync(n => n.Id == actorId);
            if (_actor != null)
            {
                _context.Actors.Remove(_actor);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Actor> UpdateActorById(ActorRequest updatedActor, Guid actorId)
        {
            var _actor = await _context.Actors.FirstOrDefaultAsync(n => n.Id == actorId);

            if (_actor != null)
            {
                _actor.Name = updatedActor.Name;
                _actor.Surname= updatedActor.Surname;
                _actor.Username= updatedActor.Username;
                _actor.Password = updatedActor.Password;
                _actor.Adress= updatedActor.Adress;
                _actor.ExpectedSalary= updatedActor.ExpectedSalary;

                await _context.SaveChangesAsync();
            }

            return _actor;
        }
    }
}
