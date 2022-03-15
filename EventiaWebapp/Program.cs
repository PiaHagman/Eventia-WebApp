var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapControllerRoute(
    "myEvents",
    "events/myevents/{id?}",
    new { controller = "Events", action = "MyEvents" });

app.MapControllerRoute(
    "joinEvent",
    "events/join",
    new { controller = "Events", action = "JoinEvent" });

app.MapControllerRoute(
    "confirmEvent",
    "events/confirm",
    new { controller = "Events", action = "ConfirmEvent" });


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
