using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunchOrder.Services.Services.IServices
{
	public interface IAuthenticationService
	{
		Task<bool> LoginAsync(string username, string password);
		Task LogoutAsync();
	}

}
