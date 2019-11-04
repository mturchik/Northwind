using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Northwind.Models;

namespace Northwind.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public AdminController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_userManager.Users);
        }
        
        [Authorize(Roles = "User")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create(CreateUser model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                ModelState.AddModelError("Email", "An account with that email already exists.");
                return View(model);
            }

            var user = new AppUser
            {
                UserName = model.Name,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
                return RedirectToAction("Index");

            //Return to Create page if create failed
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var editable = new EditUser
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email
            };
            return View(editable);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUser model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByIdAsync(model.Id);
            var passwordStatus = await _userManager.CheckPasswordAsync(user, model.CurrentPassword);
            if (!passwordStatus)
            {
                ModelState.AddModelError("Password", "The current password must be supplied correctly.");
                return View(model);
            }

            if (user.Email != model.Email)
            {
                var emailToken = await _userManager.GenerateChangeEmailTokenAsync(user, model.Email);
                await _userManager.ChangeEmailAsync(user, model.Email, emailToken);
            }

            if (user.UserName != model.Name)
                await _userManager.SetUserNameAsync(user, model.Name);
            if (!string.IsNullOrWhiteSpace(model.NewPassword))
                await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }
    }
}