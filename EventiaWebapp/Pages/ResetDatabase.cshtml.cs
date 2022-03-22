//Varför inte kunna flytta denna till viewImports?
using EventiaWebapp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventiaWebapp.Pages
{
    public class ResetDatabaseModel : PageModel
    {
        private readonly DatabaseHandler _databaseHandler;
        private readonly IWebHostEnvironment _environment;
        
        public ResetDatabaseModel(DatabaseHandler databaseHandler, IWebHostEnvironment environment)
        {
            _databaseHandler = databaseHandler;
            _environment = environment;
        }
        public IActionResult OnGet()
        {
            if (_environment.IsProduction()) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            
            await _databaseHandler.RecreateAndSeed();
            
            return Page();
        }
    }
}
