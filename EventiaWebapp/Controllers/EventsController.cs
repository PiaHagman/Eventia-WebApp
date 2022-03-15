using Microsoft.AspNetCore.Mvc;

namespace EventiaWebapp.Controllers
{
    public class EventsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyEvents()
        {
            return View("MyEvents");
        }
        public IActionResult JoinEvent()
        {
            return View("JoinEvent");
        }
        public IActionResult ConfirmEvent()
        {
            return View("ConfirmEvent");
        }
    }
}
