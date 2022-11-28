using System.ComponentModel.DataAnnotations;

namespace movie_manager.Models
{
    public class DirectorResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
