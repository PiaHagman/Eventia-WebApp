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

        //Metod som returnerar alla events
        public List<Event> GetEvents()
        {
            var eventList = ctx.Events
                .Include(e => e.Organizer)
                .ToList();

            return eventList;
        }

        public async Task <EventiaUser> GetAttendee(string userId)
        {
            var query = ctx.Users
            .Include(eu => eu.JoinedEvents)
            .ThenInclude(e => e.Organizer);

            EventiaUser eventiaUser = await query.FirstOrDefaultAsync(eu => eu.Id ==userId) ?? throw new InvalidOperationException();
            
            return eventiaUser;

        }

        public bool AttendEvent(int eventId, string id)
        {
            //Plockar fram eventet givet eventId, inkluderar attendees
            var query = ctx.Events;
                //.Include(e => e.Attendees);
            
            var evnt = query.FirstOrDefault(e => e.Id == eventId);

            if (evnt is not null)
            {
                //Plockar fram attendee
                var attendee = ctx.Users
                    .Include(eu => eu.JoinedEvents)
                    .FirstOrDefault(u => u.Id==id);
                
                attendee.JoinedEvents.Add(evnt);
                
                ctx.SaveChanges();
                return true;
            }
            return false;
        }
        //Metod som returnerar en lista på alla events som ett givet deltagarobjekt deltar i
     /*   public List<Event> GetMyEvents(int id)
        {
            var attendee = GetAttendee(id);
            var myEvents = attendee.Events.ToList();

            myEvents.Sort((date1, date2) =>
                DateTime.Compare(date1.Date, date2.Date));

            return myEvents;
        }*/
    }
}