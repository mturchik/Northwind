using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;

namespace Northwind.Controllers
{
    public class RoleAdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleAdminController(UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index() => View(_roleManager.Roles);

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Required] string name)
        {
            if (!ModelState.IsValid)
                return View(name);

            if (await _roleManager.FindByNameAsync(name) != null)
            {
                ModelState.AddModelError("Existing", "A role of that name already exists.");
                return View(name);
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(name));
            if (result.Succeeded)
                return RedirectToAction("Index");

            AddErrorsFromResult(result);
            return View(name);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var model = new EditRole
            {
                Role = role
            };
            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                    model.UsersInRole.Add(user);
                else
                    model.UsersNotInRole.Add(user);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ModifyUserRoles model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result;
                foreach (var id in model.IdsToAdd)
                {
                    var user = await _userManager.FindByIdAsync(id);
                    if (user == null) break;
                    if (await _userManager.IsInRoleAsync(user, model.RoleName)) break;

                    result = await _userManager.AddToRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                        AddErrorsFromResult(result);
                }

                foreach (var id in model.IdsToRemove)
                {
                    var user = await _userManager.FindByIdAsync(id);
                    if (user == null) break;
                    if (!await _userManager.IsInRoleAsync(user, model.RoleName)) break;

                    result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                        AddErrorsFromResult(result);
                }
            }

            if (ModelState.IsValid)
                return RedirectToAction("Index");

            return await Edit(model.RoleId);
        }

        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            foreach (var user in users)
            {
                await _userManager.RemoveFromRoleAsync(user, role.Name);
            }

            await _roleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
        }
    }
}