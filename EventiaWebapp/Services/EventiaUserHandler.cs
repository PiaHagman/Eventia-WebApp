using EventiaWebapp.Models;
using EventiaWebapp.Services.Data;
using Microsoft.AspNetCore.Identity;

namespace EventiaWebapp.Services
{
    public class EventiaUserHandler
    {

        private readonly EventiaDbContext _ctx;
        private readonly UserManager<EventiaUser> _userManager;

        public EventiaUserHandler(EventiaDbContext ctx, UserManager<EventiaUser> userManager)
        {
            _ctx=ctx;
            _userManager = userManager;
        }

        public bool UpdateRoleRequest(string userId)
        {
            var eventiaUser = _ctx.Users
                .FirstOrDefault(eu => eu.Id == userId);

            if (eventiaUser == null)
            {
                return false;
            }

            _userManager.RemoveFromRoleAsync(eventiaUser, "user");
            _userManager.AddToRoleAsync(eventiaUser, "applyingForOrganizer");
            _ctx.SaveChangesAsync();
            return true;
        }


    }
}
