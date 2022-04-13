using System.Runtime.InteropServices;
using EventiaWebapp.Models;
using EventiaWebapp.Services.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventiaWebapp.Services
{
    public class AdminHandler
    {
        private readonly EventiaDbContext _ctx;
        private readonly UserManager<EventiaUser> _userManager;

        public AdminHandler(EventiaDbContext ctx, UserManager<EventiaUser> userManager)
        {
            _ctx = ctx;
            _userManager = userManager;
        }

        public async Task<List<EventiaUser>>  GetEventiaUsers()
        {
            var users = await _ctx.Users
                .ToListAsync();
            return users;
        }

        public async Task<List<EventiaUser>> GetUsersWithApplication()
        {
            var usersList = await _ctx.Users
                .Include(eu => eu.Application)
                .Where(eu => eu.Application != null)
                .ToListAsync();
            
            return usersList;
        }

        public async Task <bool> ChangeRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            
            var roles = await _userManager.GetRolesAsync(user);

            if (roles[0] == "organizer")
            {
                await _userManager.RemoveFromRoleAsync(user, "organizer");
                await _userManager.AddToRoleAsync(user, "user");
            }

            if (roles[0] == "user")
            {
                await _userManager.RemoveFromRoleAsync(user, "user");
                await _userManager.AddToRoleAsync(user, "organizer");

                var userWithApplication = await _ctx.Users
                    .Include(eu => eu.Application)
                    .FirstOrDefaultAsync(eu => eu.Id == userId);

                if (userWithApplication == null)
                {
                    return false;
                }

                var application = await _ctx.Applications
                    .Include(a => a.Applicants)
                    .FirstOrDefaultAsync(a => a.Id == userWithApplication.Application.Id);

                if (application == null)
                {
                    return false;
                }

                _ctx.Remove(application);
            }
            
            await _ctx.SaveChangesAsync();
            return true;
        }
    }
}
