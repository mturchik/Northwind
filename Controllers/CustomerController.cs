using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;

namespace Northwind.Controllers
{
    public class CustomerController : Controller
    {
        private readonly INorthwindRepository _db;

        public CustomerController(INorthwindRepository db)
        {
            _db = db;
        }

        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {
            //Add more validation here I guess
            //And maybe redirect to an error page?
            if (string.IsNullOrWhiteSpace(customer.CompanyName) ||
                _db.Customers.Any(c => c.CompanyName == customer.CompanyName))
                return View(customer);

            //Do the database thing
            _db.AddCustomer(customer);
            return View(customer);
        }

    }
}