using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventiaWebapp.Pages
{
    [Authorize(Roles = "administrator")]
    public class ManageUsersModel : PageModel
    {
        public ManageUsersModel()
        {

        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            //[Display(Name = "Remember me?")] 
            //public List<IdentityRole> IsOrganizer { get; set; } = new List<IdentityRole>();
            public bool IsOrganizer { get; set; }
            public bool HasAppliedForOrganizer { get; set; }
        }

        public void OnGet()
        {
        }
    }
}
