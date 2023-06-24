using Microsoft.AspNetCore.Mvc;
using Super.Models;

using System.Diagnostics;

namespace Super.Areas.Admin.Controllers
{
    
    public class HomeController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
