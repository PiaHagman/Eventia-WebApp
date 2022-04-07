using EventiaWebapp.Models;
using EventiaWebapp.Services.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventiaWebapp.Services
{
    public class EventsHandler
    {
        private EventiaDbContext ctx;
        private UserManager<EventiaUser> _userManager;

        public EventsHandler(EventiaDbContext context, UserManager<EventiaUser> userManager)
        {
            ctx = context;
            _userManager = userManager;
        }

        public List<Event> GetEvents()
        {
            var eventList = ctx.Events
                .Include(e => e.Organizer)
                .ToList();

            return eventList;
        }

        public async Task<EventiaUser> GetAttendee(string userId)
        {
            var query = ctx.Users
                .Include(eu => eu.JoinedEvents)!
            .ThenInclude(e => e.Organizer);

            EventiaUser eventiaUser = await query.FirstOrDefaultAsync(eu => eu.Id ==userId) ?? throw new InvalidOperationException();
            
            return eventiaUser;

        }

        public bool AttendEvent(int eventId, string id)
        {
           
            var evnt = ctx.Events
                .FirstOrDefault(e => e.Id == eventId);

            if (evnt is not null)
            {
                var attendee = ctx.Users
                    .Include(eu => eu.JoinedEvents)
                    .FirstOrDefault(u => u.Id==id);

                if (attendee != null) attendee.JoinedEvents.Add(evnt);

                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task <List<Event>?> GetMyEvents(string id)
        {
            var attendee = await GetAttendee(id);

            if (attendee.JoinedEvents is null)
            {
                return null;
            }
            
            var myEvents = attendee.JoinedEvents.ToList();

            myEvents.Sort((date1, date2) =>
                DateTime.Compare(date1.Date, date2.Date));

            return myEvents;
        }
    }
}