using System.Reflection.Metadata.Ecma335;
using EventiaWebapp.Models;
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
        public IActionResult JoinEvent(Event evnt, Attendee attendee )
        {
            return View("JoinEvent");
        }
        public IActionResult ConfirmEvent(int id)
        {
            return View("ConfirmEvent", id);
        }
    }
}
