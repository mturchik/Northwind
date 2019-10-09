using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Models;

namespace Northwind.Controllers
{
    public class CategoryController : Controller
    {
        private readonly INorthwindRepository _db;

        public CategoryController(INorthwindRepository db)
        {
            _db = db;
        }

        public IActionResult Categories() => View(_db.Categories.Include(c => c.Products).OrderBy(c => c.CategoryName));

        public IActionResult Products(int id) => View(_db.Products.Include(p => p.Category)
            .Where(p => p.CategoryId == id && p.Discontinued == false).OrderBy(p => p.ProductName));
    }
}