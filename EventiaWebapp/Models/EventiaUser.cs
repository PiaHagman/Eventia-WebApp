using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EventiaWebapp.Models
{
    public class EventiaUser : IdentityUser
    {
        public Application? Application { get; set; }
        public ICollection <Event>? HostedEvents { get; set; }
        public ICollection<Event>? JoinedEvents { get; set; }

    }
}
