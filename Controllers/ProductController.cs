using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Models;

namespace Northwind.Controllers
{
    public class ProductController : Controller
    {
        private readonly INorthwindRepository _db;

        public ProductController(INorthwindRepository db)
        {
            _db = db;
        }

        public IActionResult Index(int id) => View(_db.Products.Include(p => p.Category)
            .Where(p => p.Discontinued == false).First(p => p.ProductId == id));
    }
}