using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using LunchOrder.Data.Data.Repository.IRepository;
using LunchOrder.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace LunchOrder.Web.Areas.Accounts.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        private readonly ApplicationDbContext  _unitOfWork;
        public LunchOrder.Services.Services.IServices.IAuthenticationService _GetAuthentication;

        public LoginModel(ILogger<LoginModel> logger, ApplicationDbContext unitOfWork, LunchOrder.Services.Services.IServices.IAuthenticationService getAuthentication)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _GetAuthentication=getAuthentication;

        }

        [BindProperty]
        public LoginViewModel Input { get; set; }

        public async void OnGet()
        {
            try
            {
                var data =  _unitOfWork.user.FirstOrDefault();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await _GetAuthentication.LoginAsync(Input.Username, Input.Password);

                if (result)
                {
                    _logger.LogInformation("User logged in.");

                    // Create a ClaimsIdentity with user information
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, Input.Username)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // Create authentication properties
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent =true, // Persist the cookie if "Remember Me" is checked
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20) // Set the expiration time
                    };

                    // Sign in the user with the cookie authentication scheme
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return LocalRedirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we reach this point, the model is invalid, so redisplay the form
            return Page();
        }

    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
