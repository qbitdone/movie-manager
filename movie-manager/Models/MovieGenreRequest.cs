using System.ComponentModel.DataAnnotations;

namespace movie_manager.Models
{
    public class MovieGenreRequest
    {
        [Required]
        public Guid MovieId { get; set; }
        [Required]
        public Guid GenreId { get; set; }
    }
}
