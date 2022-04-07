using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventiaWebapp.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Description { get; set; }
        [Required] public string Place { get; set; }
        [Required] public DateTime Date { get; set; }
        [Required] public int SeatsAvailable { get; set; }

        
        [InverseProperty("HostedEvents")]
        public EventiaUser Organizer { get; set; }

        [InverseProperty("JoinedEvents")]
        
        public ICollection<EventiaUser>? Attendees { get; set; }
    }
}
