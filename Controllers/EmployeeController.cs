using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;

namespace Northwind.Controllers
{
    public class EmployeeController : Controller
    {
           
        private readonly INorthwindRepository _db;
        private readonly UserManager<AppUser> _userManager;
        public EmployeeController(INorthwindRepository db, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _db = db;
        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(EmployeeWithPassword employeeWithPassword)
        {
            Employee employee = employeeWithPassword.Employee;
            //Add more validation here I guess
            //And maybe redirect to an error page?
            if (string.IsNullOrWhiteSpace(employee.Name) ||
                _db.Employee.Any(c => c.Name == employee.Name))
                return View(employeeWithPassword);
            if (await _userManager.FindByEmailAsync(employee.Email) != null)
            {
                ModelState.AddModelError("Email", "An account with that email already exists.");
                return View(employeeWithPassword);
            }
            if(employee.Name.Contains(' '))
            {
                ModelState.AddModelError("White", "Name can not contain spaces.");
                return View(employeeWithPassword);
            }

            //Do the database thing
            _db.AddEmployee(employee);

            var user = new AppUser
            {
                UserName = employee.Name,
                Email = employee.Email
            };
            var result = await _userManager.CreateAsync(user, employeeWithPassword.Password);
            if (result.Succeeded)
            {
                var newUser = await _userManager.FindByEmailAsync(employee.Email);
                await _userManager.AddToRoleAsync(newUser, "User");
                await _userManager.AddToRoleAsync(newUser, "Employee");
                return RedirectToAction("Account", "Account");
            }
            //Return to Create page if create failed
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return View(employee);
        }

    }
}