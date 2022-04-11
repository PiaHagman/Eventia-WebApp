using EventiaWebapp.Models;
using EventiaWebapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventiaWebapp.Pages
{
    [Authorize(Roles = "user")]
    public class ConfirmedEventModel : PageModel
    {

        private readonly EventsHandler _eventsHandler;
        public Event evnt { get; set; }

        public ConfirmedEventModel(EventsHandler eventsHandler)
        {
            _eventsHandler = eventsHandler;
        }
        public void OnGet(int eventId)
        {
            evnt = _eventsHandler.GetEvents().Find(e => e.Id == eventId);
        }

    }
}