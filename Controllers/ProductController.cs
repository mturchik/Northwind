using Microsoft.AspNetCore.Mvc;
using Northwind.Models;

namespace Northwind.Controllers
{
    public class ProductController : Controller
    {
        private readonly INorthwindRepository _db;

        public ProductController(INorthwindRepository repo)
        {
            _db = repo;
        }

        public IActionResult Category() => View(_db.Categories);
        
    }
}