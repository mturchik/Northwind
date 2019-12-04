using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> EditDiscount(string id)
        {

            return View(_db.Discounts.Include(d => d.Product).First(d => d.DiscountId == id));
        }

        public IActionResult Delete(string id)
        {
            return View();
        }
    }
}