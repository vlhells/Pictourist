using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pictourist.Models;

namespace Pictourist
{
	public class Program
	{
		public static void Main()
		{
			var builder = WebApplication.CreateBuilder();

			string connection = builder.Configuration.GetConnectionString("EfCoreBasicDatabase");

			//builder.Services.AddDbContext<PictouristContext>(options =>
			//	options.UseSqlServer(connection));

			builder.Services.AddDbContext<PictouristContext>(options => options.UseNpgsql(connection));

			builder.Services.AddIdentity<User, IdentityRole>()
				.AddEntityFrameworkStores<PictouristContext>();

			builder.Services.AddMvc();

			var app = builder.Build();

			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute("admin", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
				endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
			});

			app.Run();
		}
	}
}