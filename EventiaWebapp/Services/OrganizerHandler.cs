using EventiaWebapp.Models;
using EventiaWebapp.Services.Data;
using Microsoft.EntityFrameworkCore;

namespace EventiaWebapp.Services
{
    public class OrganizerHandler
    {
        private readonly EventiaDbContext _ctx;

        public OrganizerHandler(EventiaDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<Event>?> GetOrganizerEvents(string Organizerid)
        {
            var organizer = await _ctx.Users
                .Include(eu => eu.HostedEvents)
                .FirstOrDefaultAsync(eu => eu.Id == Organizerid);

            if (organizer.HostedEvents is null)
            {
                return null;
            }

            var hostedEvents = organizer.HostedEvents.ToList();

            hostedEvents.Sort((date1, date2) =>
                DateTime.Compare(date1.Date, date2.Date));

            return hostedEvents;
        }
    }


}
