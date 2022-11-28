using System.ComponentModel.DataAnnotations;

namespace movie_manager.Models
{
    public class UserRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
