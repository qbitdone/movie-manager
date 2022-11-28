namespace movie_manager.Models
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public double Budget { get; set; }
        public DateTime StartRecording { get; set; }
        public DateTime EndRecording { get; set; }
        public string DirectorId { get; set; }
        public Director Director { get; set; }
    }
}
