using Microsoft.AspNetCore.Mvc;
using Pictourist.Areas.Admin.Models;

namespace Pictourist.Controllers
{
	public class FriendsController : Controller
	{
		PictouristContext db;

		public FriendsController(PictouristContext db)
		{
			this.db = db;
		}

		public IActionResult Index(string? Id) // Friends/Index/Friend1, etc...
		{
			return View();
		}

		public IActionResult AddFriend(Guid Id)
		{
			var follower = db.Users.FirstOrDefault(f => f.UserName == User.Identity.Name);

			var wanted = db.Users.FirstOrDefault(u => u.Id == Id.ToString());

			follower.Friends.Add(wanted);

			db.SaveChangesAsync();

			return Content($"Вы отправили заявку на подписку на обновления {wanted.UserName}.<br>Когда пользователь её примет," +
				$"Вы сможете просматривать его фотографии.");
		}

		public IActionResult RemoveFriend(Guid Id)
		{
			var follower = db.Users.FirstOrDefault(f => f.UserName == User.Identity.Name);

			var wanted = db.Users.FirstOrDefault(u => u.Id == Id.ToString());

			follower.Friends.Remove(wanted);

			db.SaveChangesAsync();

			return Content($"Вы успешно отписались от обновлений {wanted.UserName}");
		}
	}
}
