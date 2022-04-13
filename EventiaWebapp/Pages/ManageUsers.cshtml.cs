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
        private readonly ILogger<ManageUsersModel> _logger;

        public ManageUsersModel(AdminHandler adminHandler,  
            ILogger<ManageUsersModel> logger)
        {
            _adminHandler = adminHandler;
            _logger = logger;
        }

        public List<EventiaUser> UserApplyingForOrganizer { get; set; }
        public List<EventiaUser> EventiaUsersList { get; set; }

        public void OnGet()
        {
            EventiaUsersList = _adminHandler.GetEventiaUsers();
            UserApplyingForOrganizer = _adminHandler.GetUsersWithApplication();
        }

        public async Task <IActionResult> OnPost(string userId)
        {
            bool isOrganizer = await _adminHandler.ChangeRole(userId);

            if (isOrganizer)
            {
                return RedirectToPage("/ManageUsers");

            }
            
            _logger.LogError("Role cannot be updated");
            return RedirectToPage("/Error", new
            {
                errorMessage =
                "Something went wrong, please try again."
            });
            
        }
    }
}
