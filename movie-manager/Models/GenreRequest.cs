using System.ComponentModel.DataAnnotations;

namespace movie_manager.Models
{
    public class GenreRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
