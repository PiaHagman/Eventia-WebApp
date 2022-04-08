#nullable disable
using System.ComponentModel.DataAnnotations;
using EventiaWebapp.Models;
using EventiaWebapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventiaWebapp.Pages
{
    [Authorize(Roles = "organizer")]
    public class AddEventModel : PageModel
    {
        private readonly OrganizerHandler _organizerHandler;
        private readonly UserManager<EventiaUser> _userManager;
        private readonly ILogger<AddEventModel> _logger;

        public AddEventModel(OrganizerHandler organizerHandler, UserManager<EventiaUser> userManager, ILogger<AddEventModel> logger)
        {
            _organizerHandler = organizerHandler;
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Title")]
            public string Title { get; set; }

            [Required]
            [Display(Name = "Description")]
            public string Description { get; set; }

            [Required]
            [Display(Name = "Place")]
            public string Place { get; set; }

            [Required]
            [Display(Name = "Date")]
            public DateTime Date { get; set; }

            [Required]
            [Display(Name = "Number of Seats")]
            public int NoOfSeats { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var organizer = await _userManager.GetUserAsync(User);
                bool isAdded = await _organizerHandler.AddEvent(Input, organizer);

                if (isAdded)
                {
                    return LocalRedirect("/OrganizeEvents");
                }
                else
                {
                    _logger.LogError("Event could not be added");
                    return RedirectToPage("/Error", new
                    {
                        errorMessage =
                            "This event can't be added, please try again later."
                    });
                }
            }

            return Page();
        }
    }
}
