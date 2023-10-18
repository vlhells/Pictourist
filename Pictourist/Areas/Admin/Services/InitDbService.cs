using Microsoft.AspNetCore.Identity;
using Pictourist.Admin.Models;

namespace Pictourist.Areas.Admin.Services
{
    public class InitDbService
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@admin";
            string adminPwd = "Abc1234%";

            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }

            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email=adminEmail, UserName="admin" };
                admin.SetBirthdate("2000-02-02");
                IdentityResult result = await userManager.CreateAsync(admin, adminPwd);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
