using System.ComponentModel.DataAnnotations;

namespace movie_manager.Models
{
    public class ActorRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Adress { get; set; }
        [Required]
        public double ExpectedSalary { get; set; }
    }
}
