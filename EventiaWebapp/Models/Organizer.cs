﻿using System.ComponentModel.DataAnnotations;

namespace EventiaWebapp.Models
{
    public class Organizer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}