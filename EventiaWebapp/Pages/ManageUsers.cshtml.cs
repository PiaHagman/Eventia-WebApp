using System.ComponentModel.DataAnnotations;
using EventiaWebapp.Models;
using EventiaWebapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventiaWebapp.Pages
{
    [Authorize(Roles = "administrator")]
    public class ManageUsersModel : PageModel
    {
        public AdminHandler _adminHandler;

        public ManageUsersModel(AdminHandler adminHandler)
        {
            _adminHandler = adminHandler;
        }

        public List<EventiaUser> userAppplyingForOrganizer { get; set; }

     /*   public class InputModel
        {
            //[Display(Name = "Remember me?")] 
            //public List<IdentityRole> IsOrganizer { get; set; } = new List<IdentityRole>();
            public bool IsOrganizer { get; set; }
            public bool HasAppliedForOrganizer { get; set; }
        }*/

        public void OnGet()
        {
            userAppplyingForOrganizer = _adminHandler.GetUsersWithApplication();
        }
    }
}
