using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Northwind.Controllers
{
    public class AccountController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}