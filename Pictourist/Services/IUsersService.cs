using Pictourist.Areas.Admin.Models;

namespace Pictourist.Services
{
	public interface IUsersService
	{
		public Task<IEnumerable<User>> IndexAsync();
		public Task<User> MyPageAsync(string thisUserName);
	}
}
