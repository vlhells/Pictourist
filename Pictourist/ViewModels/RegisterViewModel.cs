using Pictourist.Models;
using System.ComponentModel.DataAnnotations;

namespace Pictourist.ViewModels
{
	public class RegisterViewModel: ViewModel
    {
		public override string Login { get; set; }
	
		public override string Password { get; set; }

		[Required]
		[Compare("Password", ErrorMessage = "Пароли не совпадают")]
		[DataType(DataType.Password)]
		[Display(Name = "Подтвердить пароль")]
		public string PasswordConfirm { get; set; }
		[Required]
		public override string Email { get; set; }
        [Required]
        public override string Birthdate { get; set; }
    }
}
