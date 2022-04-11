using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventiaWebapp.Pages
{
    [AllowAnonymous]
    public class ErrorModel : PageModel
    {
        private readonly ILogger<ErrorModel> _logger;
        public string ErrorMessage;

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }
        public void OnGet(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }
    }
}
