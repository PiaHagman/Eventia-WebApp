﻿using System.ComponentModel.DataAnnotations;

namespace EventiaWebapp.Models
{
    public class Event
    {
        public int EventId { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        [Required]
        public string Place { get; set; }
        public string? Address { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public int SeatsAvailable { get; set; }

        [Required]
        public Organizer? Organizer { get; set; }

        public ICollection<AttendeeEvent>? AttendeeEvents { get; set; }
    }
}
