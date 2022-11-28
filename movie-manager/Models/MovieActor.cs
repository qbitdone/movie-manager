namespace movie_manager.Models
{
    public class MovieActor
    {
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }
        public Guid ActorId { get; set; }
        public Actor Actor { get; set; }
        public bool? DirectorAccepted { get; set; }
        public bool? ActorAccepted { get; set; }

    }
}
