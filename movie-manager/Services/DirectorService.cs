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

        public async Task<IEnumerable<Director>> GetAllDirectors() => await _context.Directors.ToListAsync();

    }
}
