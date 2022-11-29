﻿using System.ComponentModel.DataAnnotations;

namespace movie_manager.Models
{
    public class Genre
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<MovieGenre> Movies { get; set; }

    }
}
