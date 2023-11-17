using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pictourist.Areas.Admin.Models;
using Pictourist.Services;

namespace Pictourist.Controllers
{
	[Authorize]
	public class UsersController : Controller
	{
		private IUsersService _usersService;

		public UsersController(IUsersService usersService)
		{
			_usersService = usersService;
		}

		[HttpGet]
		public async Task<IActionResult> IndexAsync()
		{
			return View(await _usersService.IndexAsync());
		}

		public async Task<IActionResult> MyPageAsync()
		{
			return View("UsersPersonalPage", await _usersService.MyPageAsync(User.Identity.Name));
		}
	}
}
