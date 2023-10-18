using Pictourist.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Pictourist.ViewModels
{
    public class LoginViewModel: ViewModel
    {
        public override string Login { get; set; }

        public override string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
