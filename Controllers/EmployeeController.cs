using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;

namespace Northwind.Controllers
{
    public class EmployeeController : Controller
    {
           
        private readonly INorthwindRepository _db;

        public EmployeeController(INorthwindRepository db)
        {
            _db = db;
        }

        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            //Add more validation here I guess
            //And maybe redirect to an error page?
            if (string.IsNullOrWhiteSpace(employee.Name) ||
                _db.Employee.Any(c => c.Name == employee.Name))
                return View(employee);

            //Do the database thing
            _db.AddEmployee(employee);
            return View(employee);
        }

    }
}