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

//AddScoped - en databasconction per användare, en per http request
builder.Services.AddScoped<DatabaseHandler>();

//Funkar inte
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();



app.UseRouting();
app.MapControllerRoute(
    "myEvents",
    "events/myevents/{attId?}",
    new { controller = "Events", action = "MyEvents" });


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapRazorPages();



using (var scope = app.Services.CreateScope())
{
    var database = scope.ServiceProvider
        .GetRequiredService<DatabaseHandler>();

    if (app.Environment.IsProduction())
    {
        await database.CreateIfNotExists();
    }

    if (app.Environment.IsDevelopment())
    {
        await database.CreateAndSeedIfNotExist();
    }

    
}

app.Run();