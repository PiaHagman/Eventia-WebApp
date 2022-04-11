using EventiaWebapp.Models;
using EventiaWebapp.Services;
using EventiaWebapp.Services.Data;
    using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


//TODO Check if there are available seats and decrease number og available seats
//TODO Check how to use _viewImports better
//TODO Write report
//TODO Use ILogger and handle "nullable-situations"/Error-handling
//TODO Make sure that database is not deleted before start 
//TODO Be able to unbook events
//TODO UserName in header - name instead
//TODO Add username to regisitration
//TODO _signInManager.IsSignedIn vs User.Identity.IsAuthenticated => Been using both types in _Layout.cshtml
//TODO Skillnad?
            /*< p > Logged in as: @User.Identity.Name </ p >
            < p > @_userManager.GetUserName(User) </ p >*/
//TODO Fix default [Autorize] in program.cs
//TODO MErga in bransch

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

//Middleware-steg som tillhör Identity-paketet. M�ste ligga efter UseRouting och i den ordningen som de st�r nu.
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
        await database.Migrate();
        app.UseExceptionHandler("/Error");
    }

    if (app.Environment.IsDevelopment())
    {
        //await database.CreateAndSeedIfNotExist();
        app.UseDeveloperExceptionPage();
    }

    await database.Recreate();
    await database.Seed();

}

#endregion

#region Server start

app.Run();

#endregion