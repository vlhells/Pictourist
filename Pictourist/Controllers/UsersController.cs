using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pictourist.Areas.Admin.Models;

namespace Pictourist.Controllers
{
	[Authorize]
	public class UsersController : Controller
	{
		PictouristContext db;

		public UsersController(PictouristContext db)
		{
			this.db = db;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return View(await db.Users.Include(u => u.Friends).ToListAsync());
		}

		public IActionResult MyPage()
		{
			User u = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
			return View("UsersPersonalPage", u);
		}
	}
}
