using EventiaWebapp.Models;
using EventiaWebapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventiaWebapp.Pages
{
    [Authorize]
    public class ApplyForOrganizerModel : PageModel
    {

        private readonly UserManager<EventiaUser> _userManager;
        private readonly EventiaUserHandler _eventiaUserHandler;
        private readonly ILogger<JoinModel> _logger;

        public ApplyForOrganizerModel(UserManager<EventiaUser> userManager, EventiaUserHandler eventiaUserHandler, ILogger<JoinModel> logger)
        {
            _userManager = userManager;
            _eventiaUserHandler = eventiaUserHandler;
            _logger = logger;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            var eventiaUserId = _userManager.GetUserId(User);

            if (_eventiaUserHandler.UpdateRoleRequest(eventiaUserId))
            {
                return RedirectToPage("./ConfirmedRequest");
            }

            _logger.LogError("Can't update role to applyingForOrganizer-role");
            return RedirectToPage("/Error", new
            {
                errorMessage =
                    "This request is not valid at the moment, please try again later."
            });
        }
    }
}
