using Microsoft.EntityFrameworkCore;
using movie_manager.Models;

namespace movie_manager.Data
{
    public class MovieManagerDbContext : DbContext
    {
        public MovieManagerDbContext(DbContextOptions<MovieManagerDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
