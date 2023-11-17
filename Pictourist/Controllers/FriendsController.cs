using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pictourist.Areas.Admin.Models;
using Pictourist.Services;

namespace Pictourist.Controllers
{
	[Authorize]
	public class FriendsController : Controller
	{
		private IFriendsService _friendsService;

		public FriendsController(IFriendsService friendsService)
		{
			_friendsService = friendsService;
		}

		public async Task<IActionResult> IndexAsync(string Id) // "Friends/Index/Friend1", etc...
		{
			User u = await _friendsService.IndexAsync(User.Identity.Name, Id);
			if (u != null)
			{
				return View("~/Views/Users/UsersPersonalPage.cshtml", u);
			}

			return NoContent();
		}

		public async Task<IActionResult> IndexAsync()
		{
			return View(await _friendsService.IndexAsync());
		}

		public async Task<IActionResult> AddFriendAsync(Guid Id)
		{
			return Ok(await _friendsService.AddFriendAsync(User.Identity.Name, Id));
		}

		public async Task<IActionResult> RemoveFriendAsync(Guid Id)
		{
			return Ok(await _friendsService.RemoveFriendAsync(User.Identity.Name, Id));
		}
	}
}
