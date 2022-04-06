using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventiaWebapp.Pages
{
    public class ApplyForOrganizerModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            

            return RedirectToPage("~/");
        }
    }
}
