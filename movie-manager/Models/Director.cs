﻿using movie_manager.Services;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;

namespace movie_manager.Models
{
    public class Director
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
        public string Role { get; set; } = "Director";
        [JsonIgnore]
        public ICollection<Movie> Movies  { get; set; }
    }
}

