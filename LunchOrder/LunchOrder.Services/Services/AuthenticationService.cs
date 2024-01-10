using LunchOrder.Services.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace LunchOrder.Services.Services
{
	public class AuthenticationService : LunchOrder.Services.Services.IServices.IAuthenticationService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public AuthenticationService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<bool> LoginAsync(string username, string password)
		{
			// Implement your authentication logic here
			// Example: Check credentials against a database or any other authentication mechanism

			// For simplicity, let's assume a valid login for any non-empty username and password
			if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
			{
				var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, username),
                // Add additional claims as needed
            };

				var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var principal = new ClaimsPrincipal(identity);

				await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

				return true;
			}

			return false;
		}

		public async Task LogoutAsync()
		{
			await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
		}
	}

}
