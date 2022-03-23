using Microsoft.AspNetCore.Mvc;

namespace EventiaWebapp.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("/");
        }
    }
}
