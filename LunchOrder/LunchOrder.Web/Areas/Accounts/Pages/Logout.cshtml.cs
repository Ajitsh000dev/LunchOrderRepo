
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;


namespace LunchOrder.Web.Areas.Accounts.Pages
{
    public class LogoutModel : PageModel
    {
        public LunchOrder.Services.Services.IServices.IAuthenticationService _GetAuthentication;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(LunchOrder.Services.Services.IServices.IAuthenticationService authenticationService, ILogger<LogoutModel> logger)
        {
            _logger = logger;
            _GetAuthentication = authenticationService;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _GetAuthentication.LogoutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                // This needs to be a redirect so that the browser performs a new
                // request and the identity for the user gets updated.
                return RedirectToPage();
            }
        }
    }
}
