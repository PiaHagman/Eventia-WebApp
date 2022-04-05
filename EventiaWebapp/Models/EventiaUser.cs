using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EventiaWebapp.Models
{
    public class EventiaUser : IdentityUser
    {
       public string? FirstName { get; set; }
       public string? LastName { get; set; }

       public string? OrganizerName { get; set; }

            
        public ICollection <Event>? HostedEvents { get; set; }
        public ICollection<Event>? JoinedEvents { get; set; }

        


    }
}
