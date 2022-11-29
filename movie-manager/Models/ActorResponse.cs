using System.ComponentModel.DataAnnotations;

namespace movie_manager.Models
{
    public class ActorResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Adress { get; set; }
        public double ExpectedSalary { get; set; }
    }
}
