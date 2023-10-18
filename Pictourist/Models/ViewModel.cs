using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Pictourist.Models
{
    public abstract class ViewModel
    {
        [Required]
        [Display(Name = "Логин:")]
        public abstract string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль:")]
        public abstract string Password { get; set; }

        [Display(Name = "Email:")]
        public virtual string? Email { get; set; }

        [Display(Name = "Дата рождения:")]
        public virtual string? Birthdate { get; set; }
    }
}
