using EventiaWebapp.Models;
using EventiaWebapp.Services.Data;

namespace EventiaWebapp.Services
{
    public class EventsHandler
    {
        private EventiaDbContext _eventiaDbContext;
        public List<Event>? EventList { get; init; }
        public List<Organizer> Organizers { get; init; }


        //tjänsten tar in de andra tjänster den behöver för att kunna fungera i konstruktorn
        public EventsHandler(EventiaDbContext ctx)  
        {
            _eventiaDbContext = ctx;

            Organizers = new List<Organizer>
            {
                new() {Name = "Ticketmaster", Email = "info@ticketmaster.se", PhoneNumber = "0771-707070"},
                new() {Name = "Live Nation", Email = "info@livenation.se", PhoneNumber = "08-6650100"},
                new() {Name = "Got Event", Email = "gotevent@gotevent.se", PhoneNumber = "031-3684500"}
            };

            EventList = new List<Event>
            {
                new()
                {
                    Title = "Veronica Maggio",
                    Description =
                        "Veronica Maggio är en av landets största popstjärnor och ligger bakom stora hits som ”Hela huset”, ”Sergels torg”, ”Jag kommer” och ”Välkommen in”",
                    Place = "GÖTEBORG - Trädgårdsföreningen",
                    Date = new DateTime(2022, 08, 07),
                    SeatsAvailable = 5000,
                    Organizer = Organizers[0],
                },
                new () 
                {
                    Title = "Laleh",
                    Description = "Ingen annan artist representerar hela Sverige på samma storslagna sätt som Laleh, med sina nu dussintals hits som känns lika klassiska som moderna. ”Live Tomorrow”, ”En Stund På Jorden”, ”Some Die Young”, ”Bara Få Va Mig Själv”, ”Goliat” och nu senast ”Det kommer bli bra”!",
                    Place = "GÖTEBORG - Ullevi",
                    Date = new DateTime(2022, 06, 10),
                    SeatsAvailable = 40000,
                    Organizer = Organizers[0]
                },
                new ()
                {
                    Title = "Lady Gaga",
                    Description = "Lady Gaga, som både vunnit Grammys och Golden Globe samt nominerats till en Oscar, är en helt unik artist. Hon har sålt över 30 miljoner album och 150 miljoner singlar, vilket gör henne till en av de bäst säljande artisterna genom tiderna. ",
                    Place = "STOCKHOLM - Friends Arena",
                    Date = new DateTime(2022, 07, 22),
                    SeatsAvailable = 40000,
                    Organizer = Organizers[1]   
                },
                new ()
                {
                    Title = "Melody Gardot",
                    Description = "Melody behöver ingen närmare presentation för Sverige. Hon har själv på ett utomordentligt sätt presenterat sig för den svenska publiken under de senaste åren via sina album, TV-program och flera utsålda och hyllade konserter. ",
                    Place = "STOCKHOLM - Cirkus",
                    Date = new DateTime(2022, 06, 01),
                    SeatsAvailable = 2000,
                    Organizer = Organizers[1]
                },
                new ()
                {
                    Title = "Miriam Bryant",
                    Description = "Efter en längtan att spela live inför en publik igen ger sig Miriam Bryant äntligen ut på vägarna tillsammans med sitt band i början av nästa år. När hon återvänder till Scandinavium är det med den senaste skivan ”PS jag hatar dig” i bagaget. Albumet är hennes första på svenska där hon återigen bevisar sin styrka i både sång och text.",
                    Place = "GÖTEBORG - Scandinavium",
                    Date = new DateTime(2022, 05, 13),
                    SeatsAvailable = 20000,
                    Organizer = Organizers[2]
                },
                new ()
                {
                    Title = "Molly Sandén",
                    Description = "Efter Oscarsuccén med hyllningar världen över släppte Molly Sandén i början av maj sitt tredje album på svenska, DOM SKA VETA (MILKSHAKE/Sony Music). Ett album på elva spår med tidigare släppta låtar som Jag mår bra nu, Kärlek slutar alltid med bråk och Nån annan nu. ",
                    Place = "GÖTEBORG - Scandinavium",
                    Date = new DateTime(2022, 03, 17),
                    SeatsAvailable = 20000,
                    Organizer = Organizers[2]
                },
            };
        }
        
        //Metod som returnerar alla events
        public List<Event> GetEvents()
        {
            return EventList;
        }

        //Metod som returnerar ett default deltagarobjekt (alltid samma i denna uppgift)
        public Attendee GetAttendee()
        {
            //return _attendee;
            throw new NotImplementedException();
        }

        //Metod som registrerar ett givet deltagarobjekt med ett givet eventobjekt
        public bool AttendEvent()
        {
            //using var ctx = new EventiaDbContext();
            return true;
        }
        //Metod som returnerar en lista på alla events som ett givet deltagarobjekt deltar i
        public List<Event> GetMyEvents()
        {
            throw new NotImplementedException();
        }
    }
}
