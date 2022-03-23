using EventiaWebapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventiaWebapp.Pages
{
    public class ConfirmedEventModel : PageModel
    {

        private readonly Services.EventsHandler _eventsHandler;
        public Event evnt { get; set; }

        public ConfirmedEventModel(Services.EventsHandler eventsHandler)
        {
            _eventsHandler = eventsHandler;
        }
        public void OnGet(int eventId)
        {
            evnt = _eventsHandler.GetEvents().Find(e => e.Id == eventId);
        }

    }
}