using EventiaWebapp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventiaWebapp.Services.Data
{
    public class EventiaDbContext : DbContext
    {
        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Organizer> Organizers { get; set; }

        public EventiaDbContext(DbContextOptions options):base(options){}
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendee>()
                .HasIndex(e => e.Email).IsUnique();

            modelBuilder.Entity<Organizer>()
                .HasIndex(e => e.Name).IsUnique();

            modelBuilder.Entity<AttendeeEvent>()
                .HasKey(ae => new {ae.AttendeeId, ae.EventId});

            modelBuilder.Entity<AttendeeEvent>()
                .HasOne(ae => ae.Event)
                .WithMany(e => e.AttendeeEvents)
                .HasForeignKey(ae => ae.EventId);

            modelBuilder.Entity<AttendeeEvent>()
                .HasOne(ae => ae.Attendee)
                .WithMany(a => a.AttendeeEvents)
                .HasForeignKey(ae => ae.AttendeeId);
        
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"server=(localdb)\MSSQLLocalDB;database=EventiaDb");
        }

        
    }
}
