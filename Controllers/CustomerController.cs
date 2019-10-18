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
    }
}