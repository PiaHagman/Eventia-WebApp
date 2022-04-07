namespace EventiaWebapp.Models
{
    public class Application
    {
        public int Id { get; set; }
        private ICollection<EventiaUser>? Applicants { get; set; }

    }
}
