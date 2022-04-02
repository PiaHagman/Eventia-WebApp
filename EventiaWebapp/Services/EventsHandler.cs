﻿using EventiaWebapp.Models;
using EventiaWebapp.Services.Data;
using Microsoft.EntityFrameworkCore;

namespace EventiaWebapp.Services
{
    public class EventsHandler
    {
        private EventiaDbContext ctx;
        
        public EventsHandler(EventiaDbContext context)
        {
            ctx = context;
        }

        //Metod som returnerar alla events
        public List<Event> GetEvents()
        {
            var eventList = ctx.Events
                .Include(e => e.Organizer)
                .ToList();

            return eventList;
        }

        //Metod som returnerar ett default deltagarobjekt (alltid samma i denna uppgift)
     /*   public Attendee GetAttendee(int id)
        {

            var query = ctx.Users;
            //.Include(a => a.Events);
            //.ThenInclude(e => e.Organizer);

            Attendee attendee = query.FirstOrDefault() ?? throw new InvalidOperationException();
            return attendee;

            return null;
        }*/

        //Metod som registrerar ett givet deltagarobjekt med ett givet eventobjekt
        public bool AttendEvent(int eventId, int id)
        {

     /*       var query = ctx.Events
                .Include(e => e.Users);
            var evnt = query.FirstOrDefault(e => e.Id == eventId);

            bool evntExist = evnt != null;
            
            if (evntExist)
            {
                var attendee = ctx.Attendees.Include(a => a.Events).FirstOrDefault();
                attendee.Events.Add(evnt);
                ctx.SaveChanges();
                return true;
            }*/
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