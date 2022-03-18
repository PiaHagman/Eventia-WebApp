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
        
        public void OnGet(int id)
        {
            evnt = _eventsHandler.GetEvents().Find(e => e.EventId == id);

        }

        public IActionResult OnPost(int id)
        {
            var eventExists = _eventsHandler.AttendEvent(id);

            if (eventExists)
            {
                return RedirectToPage("ConfirmedEvent", 1);
                    
            }
            else
            {
                return NotFound("404");
            }
            
        }
    }
}
