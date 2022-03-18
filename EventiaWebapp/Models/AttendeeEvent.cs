namespace EventiaWebapp.Models
{
    public class AttendeeEvent
    {
        public int AttendeeId { get; set; }
        public Attendee Attendee { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
