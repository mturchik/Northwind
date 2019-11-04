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

        public IActionResult Index()
        {
            return View(_roleManager.Roles);
        }

        public IActionResult Create()
        {
            return View();
        }

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
            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, string name)
        {
            var roleById = await _roleManager.FindByIdAsync(id);
            if (!ModelState.IsValid)
                return View(roleById);

            var roleByName = await _roleManager.FindByNameAsync(name);
            if (roleByName != null)
            {
                ModelState.AddModelError("Name", "There is already a role by that name.");
                return View(roleById);
            }

            roleById.Name = name;
            var result = await _roleManager.UpdateAsync(roleById);
            if (result.Succeeded)
                return RedirectToAction("Index");

            AddErrorsFromResult(result);
            return View(roleById);
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

        public IActionResult AddToRole()
        {
            var model = new UsersRoles
            {
                Roles = _roleManager.Roles.ToList(),
                Users = _userManager.Users.ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(string userId, string roleName)
        {
            if (!ModelState.IsValid)
            {
                var model = new UsersRoles
                {
                    Roles = _roleManager.Roles.ToList(),
                    Users = _userManager.Users.ToList()
                };
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.AddToRoleAsync(user, roleName);
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