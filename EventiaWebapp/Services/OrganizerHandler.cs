using EventiaWebapp.Models;
using EventiaWebapp.Pages;
using EventiaWebapp.Services.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace EventiaWebapp.Services
{
    public class OrganizerHandler
    {
        private readonly EventiaDbContext _ctx;


        public OrganizerHandler(EventiaDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<Event>?> GetOrganizerEvents(string Organizerid)
        {
            var organizer = await _ctx.Users
                .Include(eu => eu.HostedEvents)
                .FirstOrDefaultAsync(eu => eu.Id == Organizerid);

            if (organizer.HostedEvents is null)
            {
                return null;
            }

            var hostedEvents = organizer.HostedEvents.ToList();

            hostedEvents.Sort((date1, date2) =>
                DateTime.Compare(date1.Date, date2.Date));

            return hostedEvents;
        }

        public async Task<bool> AddEvent(AddEventModel.InputModel model, EventiaUser organizer)
        {
            var addedEvent =
                await _ctx.Events.AddAsync(new Event
                {
                    Title = model.Title,
                    Description = model.Description,
                    Place = model.Place,
                    Date = model.Date,
                    SeatsAvailable = model.NoOfSeats,
                    Organizer = organizer
                });

            if (addedEvent != null)
            {
                await _ctx.SaveChangesAsync();
                return true;
            }

            return false;

        }

        public async Task <bool> UpdateEvent(EditEventModel.InputModel model, EventiaUser organizer, int eventId)
        {
            var evt = await _ctx.Events
                .Include(e => e.Organizer)
                .FirstOrDefaultAsync(e => e.Id == eventId);

            evt.Title = model.Title;
            evt.Description = model.Description;
            evt.Place = model.Place;
            evt.Date = model.Date;
            evt.SeatsAvailable = model.NoOfSeats;
            evt.Organizer = organizer;
            evt.Id = eventId;

            if (evt == null)
            {
                return false;
            }
            _ctx.Events.Update(evt);
            await _ctx.SaveChangesAsync();
            return true;
        }

        public async Task DeleteEvent(int eventId)
        {
            var evt = await _ctx.Events
                .FirstOrDefaultAsync(e => e.Id == eventId);

            _ctx.Events.Remove(evt);
            await _ctx.SaveChangesAsync();
        }

    }
}
