using System.ComponentModel.DataAnnotations;

namespace movie_manager.Models
{
    public class MovieActorRequest
    {
        [Required]
        public Guid MovieId { get; set; }
        [Required]
        public Guid ActorId { get; set; }
    }
}
