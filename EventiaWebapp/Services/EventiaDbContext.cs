﻿using EventiaWebapp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventiaWebapp.Services.Data
{
    public class EventiaDbContext : IdentityDbContext<EventiaUser>
    {
        public DbSet<Event> Events { get; set; }

        public EventiaDbContext(DbContextOptions <EventiaDbContext> options) : base(options) { }

       
    }
}
