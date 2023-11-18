using Microsoft.AspNetCore.Identity;
using Pictourist.Areas.Admin.Models;
using Pictourist.Areas.Admin.ViewModels;

namespace Pictourist.Areas.Admin.Services
{
	public interface IUsersService
	{
		public Task<IEnumerable<User>> IndexAsync();
		public Task<User> ChangePasswordAsync(string id);
		public Task<IdentityResult> ChangePasswordAsync(ChangePasswordViewModel model, IPasswordValidator<User> passwordValidator,
																IPasswordHasher<User> passwordHasher);
		public Task<IdentityResult> CreateAsync(CreateUserViewModel model);
		public Task<User> EditAsync(string id);
		public Task<IdentityResult> EditAsync(EditUserViewModel model);
		public Task DeleteAsync(string id);
	}
}
