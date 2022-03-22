using EventiaWebapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventiaWebapp.Pages
{
    public class JoinModel : PageModel
    {

        private readonly Services.EventsHandler _eventsHandler;
        public Event evnt { get; set; }

        public JoinModel(Services.EventsHandler eventsHandler)
        {
            _eventsHandler = eventsHandler;
        }

        public void OnGet(int eventId)
        {
            evnt = _eventsHandler.GetEvents().Find(e => e.Id == eventId);

        }

        public IActionResult OnPost(int evtId)
        {
            var attendeeId = 1;
            var eventExists = _eventsHandler.AttendEvent(evtId, attendeeId);

            if (eventExists)
            {
                return RedirectToPage("ConfirmedEvent", new {eventId = evtId});
                                     
            }
            else
            {
                return NotFound(404);
            }

        }
    }
}