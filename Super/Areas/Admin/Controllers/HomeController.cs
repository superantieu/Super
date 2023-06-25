using Microsoft.AspNetCore.Mvc;
using Super.Models;

using System.Diagnostics;

namespace Super.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin")]
    public class HomeController : Controller
    {

        
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
