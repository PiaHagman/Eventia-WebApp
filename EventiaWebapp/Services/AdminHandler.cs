using EventiaWebapp.Models;
using EventiaWebapp.Services.Data;
using Microsoft.EntityFrameworkCore;

namespace EventiaWebapp.Services
{
    public class AdminHandler
    {
        private readonly EventiaDbContext _ctx;

        public AdminHandler(EventiaDbContext ctx)
        {
            _ctx = ctx;
        }

        public List<EventiaUser> GetEventiaUsers()
        {
            var users = _ctx.Users
                //.Include(eu => eu.Rolls)
                .ToList();
            return users;
        }
    }
}
