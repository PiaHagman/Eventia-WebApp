using EventiaWebapp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventiaWebapp.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly UserManager<EventiaUser> _userManager;

        public EventsController(UserManager<EventiaUser> userManager)
        {
            _userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyEvents()
        {
          var userId = _userManager.GetUserId(User);
            return View("MyEvents", userId);
        }
        
    }
}
