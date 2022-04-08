using EventiaWebapp.Models;
using EventiaWebapp.Services.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
                .Include(eu => eu.Application)
                .FirstOrDefault(eu => eu.Id == userId);
                //Find(userID);

            if (eventiaUser == null)
            {
                return false;
            }
            
            eventiaUser.Application = new Application();
            
            _ctx.SaveChanges();
            return true;
        }


    }
}
