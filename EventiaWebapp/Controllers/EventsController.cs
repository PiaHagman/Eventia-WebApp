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
        
    }
}
