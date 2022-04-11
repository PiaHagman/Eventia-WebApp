using EventiaWebapp.Models;
using EventiaWebapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventiaWebapp.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly UserManager<EventiaUser> _userManager;
        private readonly EventsHandler _eventsHandler;
        private readonly ILogger<EventsController> _logger;

        public EventsController(UserManager<EventiaUser> userManager, EventsHandler eventsHandler, ILogger<EventsController> logger)
        {
            _userManager = userManager;
            _eventsHandler = eventsHandler;
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MyEvents()
        {
          var userId = _userManager.GetUserId(User);
            return View("MyEvents", userId);
        }

        [HttpPost]
        public async Task <IActionResult> MyEvents(string userId, int eventId)
        {
            bool evntIsCancelled = await _eventsHandler.CancelEvent(eventId, userId);
            if (evntIsCancelled)
            {
                return View("MyEvents", userId);
            }
            else
            {
                _logger.LogError("Cannot cancel event");
                return RedirectToPage("/Error", new
                {
                    errorMessage =
                        "This event can't be cancelled, please contact our customer service."
                });
            }
            
        }

    }
}
