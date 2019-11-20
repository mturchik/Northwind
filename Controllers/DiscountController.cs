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

        private INorthwindRepository _repository;

        public DiscountController(INorthwindRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index(int id)
        {
            var results = _repository.Discounts;

            if (id != 0)
            {
                results = _repository.Discounts
                    .Where(d => d.StartTime <= DateTime.Now && d.EndTime > DateTime.Now);
            }

            return View(results);
        }

        public async Task<IActionResult> AddDiscount()
        {
            return View();
        }

        public async Task<IActionResult> EditDiscount(string id)
        {
            return View();
        }

        public async Task<IActionResult> Delete(string id)
        {
            return View();
        }

    }
}