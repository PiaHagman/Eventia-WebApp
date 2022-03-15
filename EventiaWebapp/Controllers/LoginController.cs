using Microsoft.AspNetCore.Mvc;

namespace EventiaWebapp.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            //Kan jag lägga till en pathväg
            return Redirect("/");
        }
    }
}
