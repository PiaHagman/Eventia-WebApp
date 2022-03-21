using EventiaWebapp.Models;
using EventiaWebapp.Services.Data;
using Microsoft.EntityFrameworkCore;

namespace EventiaWebapp.Services
{
    public class EventsHandler
    {
        private EventiaDbContext ctx;
        public List<Event>? EventList { get; set; }
        public List<Organizer> Organizers { get; set; }


        //tjänsten tar in de andra tjänster den behöver för att kunna fungera i konstruktorn
        public EventsHandler(EventiaDbContext context)
        {
            ctx = context;

        }

        //Metod som returnerar alla events
        public List<Event> GetEvents()
        {
            using var ctx = this.ctx;
            EventList = ctx.Events.ToList();

            return EventList;
        }

        //Metod som returnerar ett default deltagarobjekt (alltid samma i denna uppgift)
        public Attendee GetAttendee()
        {
            //return _attendee;
            throw new NotImplementedException();
        }

        //Metod som registrerar ett givet deltagarobjekt med ett givet eventobjekt
        public bool AttendEvent(int eventId)
        {
            using var ctx = this.ctx;

            var query = ctx.Events.Include(e => e.Attendees);

            var evnt = query.FirstOrDefault(e => e.Id == eventId);
          

            bool evntExist = evnt != null;

            if (evntExist)
            {
                //Här inkluderar jag listan tillhörande aktuell attendee
                var attendee = ctx.Attendees.Include(a => a.Events).FirstOrDefault();

                attendee.Events.Add(evnt);

                /*ctx.Update(attendee);*/
                ctx.SaveChanges();

                return true;
            }

            return false;
        }
        //Metod som returnerar en lista på alla events som ett givet deltagarobjekt deltar i
        public List<Event> GetMyEvents()
        {
            throw new NotImplementedException();
        }
    }
}