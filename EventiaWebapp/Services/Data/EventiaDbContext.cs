using EventiaWebapp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventiaWebapp.Services.Data
{
    internal class EventiaDbContext : DbContext
    {
        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Organizer> Organizers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendee>()
                .HasIndex(e => e.Email).IsUnique();

            modelBuilder.Entity<Organizer>()
                .HasIndex(e => e.Name).IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=(localdb)\MSSQLLocalDB;database=EventiaDb");
        }

        public void Seed()
        {
            List<Organizer> organizers = new List<Organizer>
            {
                new() {Name = "Ticketmaster", Email = "info@ticketmaster.se", PhoneNumber = "0771-707070"},
                new() {Name = "Live Nation", Email = "info@livenation.se", PhoneNumber = "08-6650100"},
                new() {Name = "Got Event", Email = "gotevent@gotevent.se", PhoneNumber = "031-3684500"}
            };

            List<Attendee> attendees = new List<Attendee>
            {
                new () {Name = "Pia", Email = "hagman.pia@gmail.com", PhoneNumber = "070-4664291"}
            };

            List<Event> events = new List<Event>
            {
                new()
                {
                    Title = "Veronica Maggio",
                    Description =
                        "Veronica Maggio är en av landets största popstjärnor och ligger bakom stora hits som ”Hela huset”, ”Sergels torg”, ”Jag kommer” och ”Välkommen in”",
                    Place = "GÖTEBORG - Trädgårdsföreningen",
                    Date = new DateTime(2022, 08, 07),
                    SeatsAvailable = 5000,
                    Organizer = organizers[0],
                },
                new () 
                {
                    Title = "Laleh",
                    Description = "Ingen annan artist representerar hela Sverige på samma storslagna sätt som Laleh, med sina nu dussintals hits som känns lika klassiska som moderna. ”Live Tomorrow”, ”En Stund På Jorden”, ”Some Die Young”, ”Bara Få Va Mig Själv”, ”Goliat” och nu senast ”Det kommer bli bra”!",
                    Place = "GÖTEBORG - Ullevi",
                    Date = new DateTime(2022, 06, 10),
                    SeatsAvailable = 40000,
                    Organizer = organizers[0]
                },
                new ()
                {
                    Title = "Lady Gaga",
                    Description = "Lady Gaga, som både vunnit Grammys och Golden Globe samt nominerats till en Oscar, är en helt unik artist. Hon har sålt över 30 miljoner album och 150 miljoner singlar, vilket gör henne till en av de bäst säljande artisterna genom tiderna. ",
                    Place = "STOCKHOLM - Friends Arena",
                    Date = new DateTime(2022, 07, 22),
                    SeatsAvailable = 40000,
                    Organizer = organizers[1]   
                },
                new ()
                {
                    Title = "Melody Gardot",
                    Description = "Melody behöver ingen närmare presentation för Sverige. Hon har själv på ett utomordentligt sätt presenterat sig för den svenska publiken under de senaste åren via sina album, TV-program och flera utsålda och hyllade konserter. ",
                    Place = "STOCKHOLM - Cirkus",
                    Date = new DateTime(2022, 06, 01),
                    SeatsAvailable = 2000,
                    Organizer = organizers[1]
                },
                new ()
                {
                    Title = "Miriam Bryant",
                    Description = "Efter en längtan att spela live inför en publik igen ger sig Miriam Bryant äntligen ut på vägarna tillsammans med sitt band i början av nästa år. När hon återvänder till Scandinavium är det med den senaste skivan ”PS jag hatar dig” i bagaget. Albumet är hennes första på svenska där hon återigen bevisar sin styrka i både sång och text.",
                    Place = "GÖTEBORG - Scandinavium",
                    Date = new DateTime(2022, 05, 13),
                    SeatsAvailable = 20000,
                    Organizer = organizers[2]
                },
                new ()
                {
                    Title = "Molly Sandén",
                    Description = "Efter Oscarsuccén med hyllningar världen över släppte Molly Sandén i början av maj sitt tredje album på svenska, DOM SKA VETA (MILKSHAKE/Sony Music). Ett album på elva spår med tidigare släppta låtar som Jag mår bra nu, Kärlek slutar alltid med bråk och Nån annan nu. ",
                    Place = "GÖTEBORG - Scandinavium",
                    Date = new DateTime(2022, 03, 17),
                    SeatsAvailable = 20000,
                    Organizer = organizers[2]
                },
            };
        }
    }
}
