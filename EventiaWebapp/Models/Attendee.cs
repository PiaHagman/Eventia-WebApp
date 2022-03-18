using System.ComponentModel.DataAnnotations;

namespace EventiaWebapp.Models
{
    public class Attendee
    {
        public int AttendeeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<AttendeeEvent> AttendeeEvents { get; set; }

    }
}
