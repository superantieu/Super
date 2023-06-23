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

        //public IActionResult Index()
        //{
        //    HomeData data = new HomeData();
        //    var h = _context.Hangs.ToList();
        //    data.DSH = h;

        //    return View(data);

        //}
        public IActionResult Index(int page = 1)
        {
            // pagination: take(5) --- skip((page - 1) * 5)
            HomeData data = new HomeData();
            int limit = 10;
            int skip = ((page - 1) * limit);

            var item = _context.Hangs.OrderByDescending(k => k.MaHang)
                .Skip(skip)
                .Take(limit)
                .ToList();
            data.DSH = item;
            ViewBag.CurrentPage = page;
            return View(data);

        }

        public IActionResult _partialHang()
        {
            return PartialView();
        }
        public IActionResult _partialDanhSachHang()
        {
            return PartialView();
        }
            

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}