using EventiaWebapp.Models;
using EventiaWebapp.Services;
using EventiaWebapp.Services.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


#region Konfigurering

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

//Adds a scoped service of the type specified in EventsHandler to the specified IServiceCollection
builder.Services.AddScoped<EventsHandler>();
//AddScoped - en databasconction per användare, en per http request
builder.Services.AddScoped<DatabaseHandler>();
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}

//Hämtar default connection string i appsettings.json   
builder.Services.AddDbContext<EventiaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Det är här som Identitypaketet läggs till
builder.Services.AddDefaultIdentity<EventiaUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<EventiaDbContext>();
// .AddEntityFrameworkStores<EventiaDbContext>();


#endregion

#region Middleware Pipelining

//hur inkommande http-anrop ska hanteras i appen
var app = builder.Build();
app.UseStaticFiles(); //Ska helst ligga tidigt så slipper vi går vidare om sidan som efterfrågas är statisk.
app.UseRouting();

//Två nya middleware-steg som tillhör Identity-paketet. Måste ligga efter UseRouting och i den ordningen som de står nu.
app.UseAuthentication(); //Vem är det som skickade en http-request till oss?
app.UseAuthorization(); //Vad får personen göra?

app.MapControllerRoute(
    "myEvents",
    "myevents/{id}",
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
        //await database.CreateIfNotExists();
        app.UseExceptionHandler("/Error");
    }

    if (app.Environment.IsDevelopment())
    {
        //await database.CreateAndSeedIfNotExist();
        app.UseDeveloperExceptionPage();
    }

    //BARA NU!
    await database.RecreateAndSeed();
}

#endregion

#region Server start

app.Run();

#endregion