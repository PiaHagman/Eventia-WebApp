using System.Configuration;
using EventiaWebapp.Models;
using EventiaWebapp.Services;
using EventiaWebapp.Services.Data;
using Microsoft.EntityFrameworkCore;


#region Konfigurering

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

//Adds a scoped service of the type specified in EventsHandler to the specified IServiceCollection
builder.Services.AddScoped<EventsHandler>();

//Hämtar default connection string i appsettings.json   
builder.Services.AddDbContext<EventiaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//AddScoped - en databasconction per användare, en per http request
builder.Services.AddScoped<DatabaseHandler>();


if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}


#endregion

#region Middleware Pipelining

//hur inkommande http-anrop ska hanteras i appen
var app = builder.Build();
app.UseRouting();
app.MapControllerRoute(
    "myEvents",
    "myevents/{id}",
    new { controller = "Events", action = "MyEvents" });


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapRazorPages();

app.UseStaticFiles();

using (var scope = app.Services.CreateScope())
{
    var database = scope.ServiceProvider
        .GetRequiredService<DatabaseHandler>();

    if (app.Environment.IsProduction())
    {
        await database.CreateIfNotExists();
        app.UseExceptionHandler("/Error");
    }

    if (app.Environment.IsDevelopment())
    {
        await database.CreateAndSeedIfNotExist();
        app.UseDeveloperExceptionPage();
    }

    

}

#endregion

#region Server start

app.Run();

#endregion