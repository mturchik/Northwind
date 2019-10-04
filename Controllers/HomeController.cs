using System;
using Microsoft.AspNetCore.Mvc;

namespace Northwind.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Birthday() => View();
    }
}
