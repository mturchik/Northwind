using System;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Models;

namespace Northwind.Controllers
{
    public class HomeController : Controller
    {
        private readonly INorthwindRepository _db;
        private readonly Random _rand = new Random();

        public HomeController(INorthwindRepository db)
        {
            _db = db;
        }

        public IActionResult Index() => View(_db.Discounts
            .Include(d => d.Product)
            .Where(d => d.StartTime <= DateTime.Now && d.EndTime >= DateTime.Now)
            .OrderBy(d => 100 - _rand.Next(0, 100))
            .Take(3)); //Ensure Active Discounts

        public IActionResult Birthday() => View();
    }
}