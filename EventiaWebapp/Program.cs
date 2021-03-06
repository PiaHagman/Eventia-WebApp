using EventiaWebapp.Models;
using EventiaWebapp.Services;
using EventiaWebapp.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


//TODO Check if there are available seats and decrease number og available seats
//TODO Fix default [Authorize] in program.cs
//TODO Organizers should be able to add a picture when adding a new event.
//TODO If request for organizer has been sent, this alternative should not be visible under My Account
//TODO Improve error-handling

#region Konfigurering

   var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<EventsHandler>(); 
   builder.Services.AddScoped<EventiaUserHandler>();
   builder.Services.AddScoped<AdminHandler>();
   builder.Services.AddScoped<OrganizerHandler>();
builder.Services.AddScoped<DatabaseHandler>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}

builder.Services.AddDbContext<EventiaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Identitypaketet
builder.Services.AddDefaultIdentity<EventiaUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<EventiaDbContext>();

/*builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});*/

#endregion

#region Middleware Pipelining

var app = builder.Build();
app.UseStaticFiles(); //Ska helst ligga tidigt så slipper vi gå vidare om sidan som efterfrågas är statisk.
app.UseRouting();

//Middleware-steg som tillhör Identity-paketet. M�ste ligga efter UseRouting och i den ordningen som de står nu.
app.UseAuthentication(); 
app.UseAuthorization();

   app.MapControllerRoute(
       "myEvents",
       "myevents/{id}",
       new {controller = "Events", action = "MyEvents"});
    //.RequireAuthorization();

   app.MapControllerRoute(
       name: "default",
       pattern: "{controller}/{action=Index}/{id?}");
    //.RequireAuthorization();

   app.MapRazorPages();
    //.RequireAuthorization();

using (var scope = app.Services.CreateScope())
{
    var database = scope.ServiceProvider
        .GetRequiredService<DatabaseHandler>();

    if (app.Environment.IsProduction())
    {
        //await database.Migrate();
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