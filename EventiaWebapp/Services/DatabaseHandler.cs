using EventiaWebapp.Models;
using EventiaWebapp.Services.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventiaWebapp.Services
{
    public class DatabaseHandler
    {
        private readonly EventiaDbContext _ctx;
        private readonly UserManager<EventiaUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DatabaseHandler(EventiaDbContext ctx, UserManager<EventiaUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _ctx = ctx;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task RecreateAndSeed()
        {
            await Recreate();
            await Seed();

        }

        public async Task Recreate()
        {
            await _ctx.Database.EnsureDeletedAsync();
            await _ctx.Database.EnsureCreatedAsync();
        }


        public async Task CreateIfNotExists()
        {
            await _ctx.Database.EnsureCreatedAsync();
        }

        public async Task Migrate()
        {
            await _ctx.Database.MigrateAsync();
        }

        //developerMode
        public async Task CreateAndSeedIfNotExist()
        {
            bool didCreateDatabase = await _ctx.Database.EnsureCreatedAsync();
            if (didCreateDatabase)
            {
                await Seed();
            }
        }

        
        public async Task Seed()
        {

            List<Event> events = new List<Event>
                {
                    new()
                    {
                        Image = "../images/veronica.jpg",
                        Title = "Veronica Maggio",
                        Description =
                            "Veronica Maggio är en av landets största popstjärnor och ligger bakom stora hits som ”Hela huset”, ”Sergels torg”, ”Jag kommer” och ”Välkommen in”",
                        Place = "GÖTEBORG - Trädgårdsföreningen",
                        Date = new DateTime(2022, 08, 07, 19, 30, 00),
                        SeatsAvailable = 5000,
                    },
                    new()
                    {
                        Image = "../images/laleh.jpg",
                        Title = "Laleh",
                        Description =
                            "Ingen annan artist representerar hela Sverige på samma storslagna sätt som Laleh, med sina nu dussintals hits som känns lika klassiska som moderna. ”Live Tomorrow”, ”En Stund På Jorden”, ”Some Die Young”, ”Bara Få Va Mig Själv”, ”Goliat” och nu senast ”Det kommer bli bra”!",
                        Place = "GÖTEBORG - Ullevi",
                        Date = new DateTime(2022, 06, 10, 19, 00, 00),
                        SeatsAvailable = 40000,
                       
                    },
                    new()
                    {
                        Image = "../images/gaga.jpg",
                        Title = "Lady Gaga",
                        Description =
                            "Lady Gaga, som både vunnit Grammys och Golden Globe samt nominerats till en Oscar, är en helt unik artist. Hon har sålt över 30 miljoner album och 150 miljoner singlar, vilket gör henne till en av de bäst säljande artisterna genom tiderna. ",
                        Place = "STOCKHOLM - Friends Arena",
                        Date = new DateTime(2022, 07, 22, 20, 30, 00),
                        SeatsAvailable = 40000,
                       
                    },
                    new()
                    {
                        Image = "../images/melody.jpg",
                        Title = "Melody Gardot",
                        Description =
                            "Melody behöver ingen närmare presentation för Sverige. Hon har själv på ett utomordentligt sätt presenterat sig för den svenska publiken under de senaste åren via sina album, TV-program och flera utsålda och hyllade konserter. ",
                        Place = "STOCKHOLM - Cirkus",
                        Date = new DateTime(2022, 06, 01, 17, 00, 00),
                        SeatsAvailable = 2000,
                        
                    },
                    new()
                    {
                        Image = "../images/miriam.jpg",
                        Title = "Miriam Bryant",
                        Description =
                            "Efter en längtan att spela live inför en publik igen ger sig Miriam Bryant äntligen ut på vägarna tillsammans med sitt band i början av nästa år. När hon återvänder till Scandinavium är det med den senaste skivan ”PS jag hatar dig” i bagaget. Albumet är hennes första på svenska där hon återigen bevisar sin styrka i både sång och text.",
                        Place = "GÖTEBORG - Scandinavium",
                        Date = new DateTime(2022, 05, 13, 15, 00, 00),
                        SeatsAvailable = 20000,
                        
                    },
                    new()
                    {
                        Image = "../images/molly.jpg",
                       Title = "Molly Sandén",
                        Description =
                            "Efter Oscarsuccén med hyllningar världen över släppte Molly Sandén i början av maj sitt tredje album på svenska, DOM SKA VETA (MILKSHAKE/Sony Music). Ett album på elva spår med tidigare släppta låtar som Jag mår bra nu, Kärlek slutar alltid med bråk och Nån annan nu. ",
                        Place = "GÖTEBORG - Scandinavium",
                        Date = new DateTime(2022, 08, 17, 18, 30, 00),
                        SeatsAvailable = 20000,
                        
                    },
                };

            var ticketMasterEvents = new List<Event> { events[0], events[1], events[2], events[3], events[4], events[5]};
            var märtasJoinedEvents = new List<Event> { events[4], events[5]};


            List<EventiaUser> eventiaUsers = new List<EventiaUser>
            {
                new() {Email = "info@ticketmaster.se", UserName = "Ticketmaster", HostedEvents = ticketMasterEvents},
                new() {Email = "hagman.pia@gmail.com", UserName = "AdminPia"},
                new() {Email = "johan@gmail.com", UserName = "JohansEvents", Application = new Application()},
                new() {Email = "marta@gmail.com", UserName = "Marta", JoinedEvents = märtasJoinedEvents }
            };

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new() {Name = "user"},  
                new() {Name = "organizer"},
                new() {Name = "administrator"},
            };

            await _ctx.AddRangeAsync(events);
            await _ctx.SaveChangesAsync();

            await _userManager.CreateAsync(eventiaUsers[0], "*Ett2345");
            await _userManager.CreateAsync(eventiaUsers[1], "*Ett2345");
            await _userManager.CreateAsync(eventiaUsers[2], "*Ett2345");
            await _userManager.CreateAsync(eventiaUsers[3], "*Ett2345");

            await _roleManager.CreateAsync(roles[0]);
            await _roleManager.CreateAsync(roles[1]);
            await _roleManager.CreateAsync(roles[2]);

            await _userManager.AddToRoleAsync(eventiaUsers[0], $"{roles[1]}"); //Ticketmaster = Organizer
            await _userManager.AddToRoleAsync(eventiaUsers[1], $"{roles[2]}"); //Pia = Administrator
            await _userManager.AddToRoleAsync(eventiaUsers[2], $"{roles[0]}"); //Johan = Attendee
            await _userManager.AddToRoleAsync(eventiaUsers[3], $"{roles[0]}"); //Märta = Attendee
        }
    }
}
