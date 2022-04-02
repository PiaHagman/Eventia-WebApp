using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EventiaWebapp.Models
{
    public class EventiaUser : IdentityUser
    {
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }

        
        public virtual ICollection <Event> HostedEvents { get; set; }

        public virtual ICollection<Event> JoinedEvents { get; set; }

        


    }
}
