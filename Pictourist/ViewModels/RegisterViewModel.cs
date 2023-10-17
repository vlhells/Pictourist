using System.ComponentModel.DataAnnotations;

namespace Pictourist.ViewModels
{
	public class RegisterViewModel
	{
		[Required]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[Display(Name = "Логин")]
		public string Login { get; set; }

		[Required]
		[Display(Name = "Дата рождения")]
		public string Birthdate { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Пароль")]
		public string Password { get; set; }

		[Required]
		[Compare("Password", ErrorMessage = "Пароли не совпадают")]
		[DataType(DataType.Password)]
		[Display(Name = "Подтвердить пароль")]
		public string PasswordConfirm { get; set; }
	}
}
