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
    public class EditEventModel : PageModel
    {
        private readonly EventsHandler _eventsHandler;
        private readonly UserManager<EventiaUser> _userManager;
        private readonly OrganizerHandler _organizerHandler;
        private readonly ILogger<AddEventModel> _logger;

        
        public Event Evnt { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

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
        

        public EditEventModel(EventsHandler eventsHandler, UserManager<EventiaUser> userManager, OrganizerHandler organizerHandler, ILogger<AddEventModel> logger)
        {
            _eventsHandler = eventsHandler;
            _userManager = userManager;
            _organizerHandler = organizerHandler;
            _logger= logger;
        }
        public async Task OnGet(int eventId)
        {
            Input = new InputModel();
            Evnt = await _eventsHandler.GetEvent(eventId);

            Input.Title = Evnt.Title;
            Input.Description = Evnt.Description;
            Input.Place = Evnt.Place;
            Input.Date = Evnt.Date;
            Input.NoOfSeats = Evnt.SeatsAvailable;
        }

        public async Task<IActionResult> OnPostAsync(int eventId)
        {
            if (ModelState.IsValid)
            {
                var organizer = await _userManager.GetUserAsync(User);

                bool isUpdated = await _organizerHandler.UpdateEvent(Input, organizer, eventId);

                if (isUpdated)
                {
                    return LocalRedirect("/OrganizeEvents");
                }
                else
                {
                    _logger.LogError("Event could not be edited");
                    return RedirectToPage("/Error", new
                    {
                        errorMessage =
                            "This event was not edited, please try again later."
                    });
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDelete(int eventId)
        {
            await _organizerHandler.DeleteEvent(eventId);
            return LocalRedirect("/OrganizeEvents");
        }
    }
}
