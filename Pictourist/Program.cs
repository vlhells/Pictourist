using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pictourist.Areas.Admin.Models;
using Pictourist.Areas.Admin.Services;
using Pictourist.Services;

namespace Pictourist
{
    public class Program
	{
		public static async Task Main()
		{
			//TODO:
			// Friends-AJAX.
			// Swagger.
			// Docker.
			// Tests.

			// Upload and watch images.
			//Chattin w/ Signal(?)
			//Likes/Comms.

			//Refactor.

			var builder = WebApplication.CreateBuilder();

			string connection = builder.Configuration.GetConnectionString("DefaultConnection");

			//builder.Services.AddDbContext<PictouristContext>(options =>
			//	options.UseSqlServer(connection));

			builder.Services.AddSwaggerGen();

			builder.Services.AddDbContext<PictouristContext>(options => options.UseNpgsql(connection));

            builder.Services.AddTransient<IUserValidator<User>, MyUserValidator>();

			builder.Services.AddScoped<IFriendsService, FriendsService>();
			builder.Services.AddScoped<IAccountService, AccountService>();
			builder.Services.AddScoped<IRolesService, RolesService>();
			builder.Services.AddScoped<Pictourist.Areas.Admin.Services.IUsersService, 
										Pictourist.Areas.Admin.Services.UsersService>();
			builder.Services.AddScoped<Pictourist.Services.IUsersService,
							Pictourist.Services.UsersService>();

			//builder.Services.AddTransient<IPasswordValidator<User>, MyPasswordValidator>();

			builder.Services.AddIdentity<User, IdentityRole>(opts =>
			{
				opts.User.RequireUniqueEmail = true;
			})
			.AddEntityFrameworkStores<PictouristContext>();

			builder.Services.AddMvc();

            var app = builder.Build();

			app.UseStatusCodePages();

			app.UseStaticFiles();

			app.UseSwagger();
			app.UseSwaggerUI();

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