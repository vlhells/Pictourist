using Microsoft.AspNetCore.Identity;
using Pictourist.ViewModels;
using System.Text.RegularExpressions;

namespace Pictourist.Models
{
	public class User: IdentityUser
	{
		//public int Id { get; private set; }
		//public Role Role { get; private set; }
		//public string Login { get; private set; }
		//public Email Email { get; private set; }
		//public string Password { get; private set; }

		//public Birthdate Birthdate { get; private set; }
		public string Birthdate { get; private set; }
		public List<User> Friends { get; private set; }
		private List<string> _userPhotos { get; set; }
		public IReadOnlyCollection<string> UserPhotos => _userPhotos.AsReadOnly();

		public void AddFriend(User user)
		{
			Friends.Add(user);
		}

		private User()
		{

		}

		public User(RegisterViewModel model)
		{
			_userPhotos = new List<string>();
			Friends = new List<User>();
			Birthdate = model.Birthdate.ToString();
			Email = model.Email;
			UserName = model.Login;
		}

		public void SetBirthdate(string date)
		{
			////Regex
			////string[] day_month_year = date.Split('.'); // 01.01.2001

			////if (day_month_year.Count() == 3 && String.IsNumer)
			////{
			////	Birthdate = new Birthdate(date);
			////}

			//string pattern = "[0-3][0-9].[";
			//if (Regex.IsMatch(date, pattern))
			//{
			//	Birthdate = new Birthdate(date);
			//}
		}
	}

	//public class Birthdate
	//{
	//	public Birthdate(string date)
	//	{
	//		Date = date;
	//	}

	//	private string Date { get; set; }

		
	//	private Birthdate()
	//	{

	//	}
	//}
}
