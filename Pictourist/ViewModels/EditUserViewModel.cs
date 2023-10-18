using Pictourist.Models;
using System.ComponentModel.DataAnnotations;

namespace Pictourist.ViewModels
{
    public class EditUserViewModel: ViewModel
    {
        public string Id { get; set; }
        public override string Login { get; set; }
        public override string Password { get; set; }
        [Required]
        public override string Email { get; set; }
        [Required]
        public override string Birthdate { get; set; }
    }
}
