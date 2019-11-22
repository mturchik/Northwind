using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;
using Northwind.Models;

namespace Northwind.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize]
        public async Task<IActionResult> Account()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(user);
        }

        public async Task<IActionResult> Login(LoginUser login)
        {
            if (login == null)
                return View();
            if (!ModelState.IsValid)
                return View();

            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found.");
                return View();
            }

            await _signInManager.SignOutAsync();
            var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
            if (result.Succeeded)
                return Redirect("/Account/Account");
            
            ModelState.AddModelError("", "Invalid user or password.");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}