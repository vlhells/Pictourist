using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pictourist.Areas.Admin.Models;

namespace Pictourist.Controllers
{
	[Authorize]
	public class FriendsController : Controller
	{
		PictouristContext db;

		public FriendsController(PictouristContext db)
		{
			this.db = db;
		}

		public async Task<IActionResult> Index(string? Id) // "Friends/Index/Friend1", etc...
		{
			if (Id != null)
			{
				User u = db.Users.FirstOrDefault(x => x.Id == Id);
				if (u != null)
				{
					return View("~/Views/Users/UsersPersonalPage.cshtml", u);
				}
			}

			var authedUser = db.Users.Include(u => u.Friends).FirstOrDefault(u => u.UserName == User.Identity.Name); 

			return View(await db.Users.Include(u => u.Friends).
				Where(u => (u.Friends.Contains(authedUser) && authedUser.Friends.Contains(u))).
				ToListAsync());
		}

		public IActionResult AddFriend(Guid Id)
		{
			var follower = db.Users.FirstOrDefault(f => f.UserName == User.Identity.Name);

			var wanted = db.Users.FirstOrDefault(u => u.Id == Id.ToString());

			follower.Friends.Add(wanted);

			db.SaveChangesAsync();

			return Ok($"Вы отправили заявку на подписку на обновления {wanted.UserName}.<br>Когда пользователь её примет," +
				$"Вы сможете просматривать его фотографии.");
		}

		public IActionResult RemoveFriend(Guid Id)
		{
			var follower = db.Users.Include(u => u.Friends).FirstOrDefault(f => f.UserName == User.Identity.Name);

			var wanted = db.Users.Include(u => u.Friends).FirstOrDefault(u => u.Id == Id.ToString());

			follower.Friends.Remove(wanted);

			db.SaveChangesAsync();

			return Ok($"Вы успешно отписались от обновлений {wanted.UserName}");
		}
	}
}
