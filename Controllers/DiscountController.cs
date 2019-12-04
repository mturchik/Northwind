using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;

namespace Northwind.Controllers
{
    public class DiscountController : Controller
    {
        private readonly INorthwindRepository _db;

        public DiscountController(INorthwindRepository db)
        {
            _db = db;
        }

        public IActionResult Index() => View(_db.Discounts.OrderBy(d => d.EndTime));

        public IActionResult AddDiscount() => View();

        //todo: Create views
        //todo: Implement AJAX in views
        public IActionResult EditDiscount(string id)
        {
            return View();
        }

        public async Task<IActionResult> Delete(string id)
        {
            return View();
        }
    }
}