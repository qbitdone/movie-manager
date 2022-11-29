﻿using System.ComponentModel.DataAnnotations;

namespace movie_manager.Models
{
    public class MovieActor
    {
        [Required]
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }
        [Required]
        public Guid ActorId { get; set; }
        public Actor Actor { get; set; }
        public bool? DirectorAccepted { get; set; }
        public bool? ActorAccepted { get; set; }

    }
}
