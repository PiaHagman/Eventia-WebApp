using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventiaWebapp.Pages
{
    [Authorize]
    public class ConfirmedRequestModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
