using Microsoft.AspNetCore.Mvc;

namespace EventiaWebapp.Controllers
{
    public class EventsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyEvents(int id)
        {
            return View("MyEvents", id);
        }
        public IActionResult JoinEvent(int id)
        {
            return View("JoinEvent");
        }
        public IActionResult ConfirmEvent(int id)
        {
            return View("ConfirmEvent", id);
        }
    }
}
