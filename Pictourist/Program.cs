using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Pictourist.Admin.Models;
using Pictourist.Areas.Admin.Services;

namespace Pictourist
{
    public class Program
	{
		public static async Task Main()
		{
			//TODO:
			// Custom required messsages, translate validation messages, 
			// Styles,
			// Friends,
			// Upload and watch images.
			// Refactor.

			var builder = WebApplication.CreateBuilder();

			string connection = builder.Configuration.GetConnectionString("EfCoreBasicDatabase");

			//builder.Services.AddDbContext<PictouristContext>(options =>
			//	options.UseSqlServer(connection));

			builder.Services.AddDbContext<PictouristContext>(options => options.UseNpgsql(connection));

			builder.Services.AddIdentity<User, IdentityRole>(opts => opts.User.RequireUniqueEmail = true)
				.AddEntityFrameworkStores<PictouristContext>();

			builder.Services.AddMvc();

			var app = builder.Build();

			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute("admin", "{area:exists}/{controller=users}/{action=Index}/{id?}");
				endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            using (var scope = app.Services.CreateScope()) // Init db w/ roles and admin if db clean.
            {
                var services = scope.ServiceProvider;

                try
                {
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    await InitDbService.InitializeAsync(userManager, rolesManager);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            app.Run();
		}
	}
}