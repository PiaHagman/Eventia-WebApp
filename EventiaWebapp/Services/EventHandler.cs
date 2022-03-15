using EventiaWebapp.Models;

namespace EventiaWebapp.Services
{
    public class EventHandler
    {
        public List<Event>? EventList { get; init; }
        private Attendee _attendee;


        //Använda konstruktor?
        public EventHandler(Attendee attendee)
        {
            _attendee = attendee;
        }
        
        //Metod som returnerar alla events
        public List<Event> GetEvents()
        {
            return EventList;
        }

        //Metod som returnerar ett default deltagarobjekt (alltid samma i denna uppgift)
        public Attendee GetAttendee()
        {
            return _attendee;
        }

        //Metod som registrerar ett givet deltagarobjekt med ett givet eventobjekt
        public bool AttendEvent()
        {
            //using var ctx = new EventiaDbContext();
            return true;
        }
        //Metod som returnerar en lista på alla events som ett givet deltagarobjekt deltar i
        public List<Event> GetMyEvents()
        {
            throw new NotImplementedException();
        }
    }
}
