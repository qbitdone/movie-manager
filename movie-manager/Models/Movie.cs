using System.ComponentModel.DataAnnotations;

namespace movie_manager.Models
{
    public class Movie
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Duration { get; set; }
        [Required]
        public double Budget { get; set; }
        [Required]
        public DateTime StartRecording { get; set; }
        [Required]
        public DateTime EndRecording { get; set; }
        [Required]
        public Guid DirectorId { get; set; }
        public Director Director { get; set; }
        public ICollection<MovieActor> Actors { get; set; }
        public ICollection<MovieGenre> Genres { get; set; }
    }
}
