using EventiaWebapp.Models;
using EventiaWebapp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventiaWebapp.Pages
{
    public class OrganizeEventsModel : PageModel
    {
        public UserManager<EventiaUser> _userManager;
        public OrganizerHandler _organizerHandler;
        private readonly ILogger<OrganizeEventsModel> _logger;

        public List<Event>? HostedEvents { get; set; }

        public OrganizeEventsModel(UserManager<EventiaUser> userManager, 
            OrganizerHandler organizerHandler,
            ILogger<OrganizeEventsModel> logger)
        {
            _userManager = userManager;
            _organizerHandler = organizerHandler;
            _logger = logger;
        }

        public async Task OnGet()
        {
            string organizerId = _userManager.GetUserId(User);
            
            HostedEvents = await _organizerHandler.GetOrganizerEvents(organizerId);
            
            if (HostedEvents == null)
            {
                _logger.LogError("Event is missing");
            }
        }
    }
}
