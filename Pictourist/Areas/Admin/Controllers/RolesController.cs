using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pictourist.Areas.Admin.Models;
using Pictourist.Areas.Admin.Services;

namespace Pictourist.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }
        public async Task<IActionResult> IndexAsync() => View(await _rolesService.IndexAsync());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> CreateAsync(string name)
        {
			IdentityResult result = await _rolesService.CreateAsync(name);
			if (result.Succeeded)
			{
				return RedirectToAction("Index");
			}
			else
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}
			return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _rolesService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UserList() => View(await _rolesService.UserList());

        public async Task<IActionResult> EditAsync(string userId)
        {
            var model = await _rolesService.EditAsync(userId);
            if (model != null)
            {
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(string userId, List<string> roles)
        {
            User user = await _rolesService.EditAsync(userId, roles);
            if (user != null)
            {
                return RedirectToAction("UserList");
            }

            return NotFound();
        }
    }
}
