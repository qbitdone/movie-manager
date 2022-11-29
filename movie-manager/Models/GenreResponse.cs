using System.ComponentModel.DataAnnotations;

namespace movie_manager.Models
{
    public class GenreResponse
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
