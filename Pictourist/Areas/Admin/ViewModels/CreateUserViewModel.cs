using Pictourist.Models;
using System.ComponentModel.DataAnnotations;

namespace Pictourist.Areas.Admin.ViewModels
{
    public class CreateUserViewModel : ViewModel
    {
        public override string Login { get; set; }

        [Required(ErrorMessage = $"{_errorMessage}")]
        [DataType(DataType.Password)]
        public override string Password { get; set; }
        [Required]
        public override string Email { get; set; }
        [Required]
        public override string Birthdate { get; set; }
    }
}
