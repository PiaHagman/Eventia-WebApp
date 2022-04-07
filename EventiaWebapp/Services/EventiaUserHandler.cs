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
                //Find(userID);

            if (eventiaUser == null)
            {
                return false;
            }
            
            //Funkar ej!!
            //_ctx.Applications.Add(new Application());
            //eventiaUser.Application = new Application();
            _ctx.Applications.Add(new Application());

            /*_UserManager.RemoveFromRoleAsync(eventiaUser, "user");
            _UserManager.AddToRoleAsync(eventiaUser, "applyingForOrganizer");*/
            _ctx.SaveChanges();
            return true;
        }


    }
}
