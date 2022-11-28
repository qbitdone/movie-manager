using System.ComponentModel.DataAnnotations;

namespace movie_manager.Models
{
    public class MovieGenre
    {
        [Required]
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }
        [Required]
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
