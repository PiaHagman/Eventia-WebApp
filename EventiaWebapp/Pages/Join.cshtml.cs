using EventiaWebapp.Models;
using EventiaWebapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventiaWebapp.Pages
{
    [Authorize]
    public class JoinModel : PageModel
    {

        private readonly EventsHandler _eventsHandler;
        private readonly ILogger<JoinModel> _logger;
        private readonly UserManager<EventiaUser> _userManager;

        [BindProperty]
        public Event? Evnt { get; set; } 

        public JoinModel(ILogger<JoinModel> logger, EventsHandler eventsHandler, UserManager<EventiaUser> userManager)
        {
            _eventsHandler = eventsHandler;
            _logger = logger;
            _userManager = userManager;
        }

        public void OnGet(int eventId)
        {
            Evnt = _eventsHandler.GetEvents().Find(e => e.Id == eventId);

        }

        public IActionResult OnPost(int evtId)
        {
            var userId = _userManager.GetUserId(User); 

            var eventExists = _eventsHandler.AttendEvent(evtId, userId);
            
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