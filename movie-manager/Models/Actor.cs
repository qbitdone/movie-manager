using System.ComponentModel.DataAnnotations;

namespace movie_manager.Models
{
    public class Actor
    {
        public Guid Id { get; set; }
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
        public string Role { get; set; } = "Actor";
        public ICollection<MovieActor> Movies { get; set; }

    }
}
