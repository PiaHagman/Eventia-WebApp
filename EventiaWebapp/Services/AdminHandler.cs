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

        public List<EventiaUser> GetEventiaUsers()
        {
            var users = _ctx.Users
                .ToList();
            return users;
        }

        public List<EventiaUser> GetUsersWithApplication()
        {
            var usersList = _ctx.Users
                .Include(eu => eu.Application)
                .Where(eu => eu.Application != null)
                .ToList();
            
            return usersList;
        }

        public async Task <bool> UpgradeToOrganizer(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
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
            await _ctx.SaveChangesAsync();
            
            return true;
        }
    }
}
