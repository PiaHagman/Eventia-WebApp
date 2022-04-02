
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace EventiaWebapp.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
