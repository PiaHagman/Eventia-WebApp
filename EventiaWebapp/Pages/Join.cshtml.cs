using EventiaWebapp.Models;
using EventiaWebapp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventiaWebapp.Pages
{
    public class JoinModel : PageModel
    {

        private readonly EventsHandler _eventsHandler;
        public Event evnt { get; set; }
        private readonly ILogger<JoinModel> _logger;

        public JoinModel(ILogger<JoinModel> logger, EventsHandler eventsHandler)
        {
            _eventsHandler = eventsHandler;
            _logger = logger;
        }

        public void OnGet(int eventId)
        {
            evnt = _eventsHandler.GetEvents().Find(e => e.Id == eventId);

        }

        public IActionResult OnPost(int evtId)
        {
            //TODO kolla om användaren redan är registrerad på event och i så fall ge ett felmeddelande. En alert kanske? 
            
            var attendeeId = 1;

            
            

            var eventExists = _eventsHandler.AttendEvent(evtId, attendeeId);

            if (eventExists)
            {
                return RedirectToPage("ConfirmedEvent", new {eventId = evtId});
                                     
            }
            else
            {
                _logger.LogError("Event is missing");
                return RedirectToPage("/Error", new {errorMessage = 
                    "This event can't be found, please try again."});
            }

        }
    }
}