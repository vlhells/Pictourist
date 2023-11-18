using Pictourist.Areas.Admin.Models;

namespace Pictourist.Services
{
	public interface IFriendsService
	{
		public Task<IEnumerable<User>> IndexAsync();

		public Task<User> IndexAsync(string authedName, string Id);

		public Task<string> AddFriendAsync(string authedName, Guid Id);

		public Task<string> RemoveFriendAsync(string authedName, Guid Id);
	}
}
