using Microsoft.AspNetCore.Mvc;
using Super.Models;
using System.Diagnostics;

namespace Super.Controllers
{
    public class HomeController : Controller
    {
        private readonly QlbhContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, QlbhContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var item = _context.Hangs.ToList();
            return View(item);
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}