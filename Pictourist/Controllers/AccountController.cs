using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pictourist.Areas.Admin.Models;
using Pictourist.Services;
using Pictourist.ViewModels;

namespace Pictourist.Controllers
{
    public class AccountController : Controller
	{
        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePasswordAsync(string OldPassword, string NewPassword)
        {
            var errors = string.Empty;

			var result = await _accountService.ChangePasswordAsync(User.Identity.Name, OldPassword, NewPassword);

			if (result.Succeeded)
            {
                return Ok("Вы успешно сменили пароль.");
            }
            else
            {
                //ModelState.AddModelError(string.Empty, "Неверный пароль");
                //foreach (var error in result.Errors)
                //{
                //    errors += $"{error.Description}<br>";
                //}
                errors += "Неверный пароль";
            }

            return Content(errors);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _accountService.LoginAsync(model);
                if (result.Succeeded)
                {
                    // Принадлежит ли URL приложению.
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неверный логин и (или) пароль");
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutAsync()
        {
            await _accountService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _accountService.RegisterAsync(model);
				if (result != null)
				{
					return RedirectToAction("Index", "Home");
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}
			}
			return View(model);
		}
	}
}
