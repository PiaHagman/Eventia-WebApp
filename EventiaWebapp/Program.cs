using EventiaWebapp.Models;
using EventiaWebapp.Services;
using EventiaWebapp.Services.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

//Adds a scoped service of the type specified in EventsHandler to the specified IServiceCollection
builder.Services.AddScoped<EventsHandler>();

//Registers the given context as a service in the IServiceCollection
builder.Services.AddDbContext<EventiaDbContext>(options =>
    options.UseSqlServer(@"server=(localdb)\MSSQLLocalDB;database=EventiaDb"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedTestData.Initialize(services); //Services är en IServiceProvider 
}

app.MapControllerRoute(
    "myEvents",
    "events/myevents/{attId?}",
    new { controller = "Events", action = "MyEvents" });

app.MapControllerRoute(
    "joinEvent",
    "events/join/{eventId}",
    new { controller = "Events", action = "JoinEvent" });

app.MapControllerRoute(
    "confirmEvent",
    "events/confirm",
    new { controller = "Events", action = "ConfirmEvent" });


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
