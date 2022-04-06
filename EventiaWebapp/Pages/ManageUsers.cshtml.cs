using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventiaWebapp.Pages
{
    [Authorize(Roles = "administrator")]
    public class ManageUsersModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
