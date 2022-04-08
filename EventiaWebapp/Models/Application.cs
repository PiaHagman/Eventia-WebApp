namespace EventiaWebapp.Models
{
    public class Application
    {
        public int Id { get; set; }
        public ICollection<EventiaUser>? Applicants { get; set; }

    }
}
