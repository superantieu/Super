using Microsoft.AspNetCore.Mvc;
using Super.Models;
using System.Diagnostics;
using System.Drawing.Printing;
using X.PagedList;

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
        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            // pagination: take(5) --- skip((page - 1) * 5)
            //HomeData data = new HomeData();
            //int limit = 10;
            //int skip = ((page - 1) * limit);

            //var item = _context.Hangs.OrderByDescending(k => k.MaHang).Where(c => c.IsActive == true)
            //    .Skip(skip)
            //    .Take(limit)
            //    .ToList();
            //data.DSH = item;
            //ViewBag.CurrentPage = page;
            //return View(data);
            var model = _context.Hangs.OrderByDescending(x => x.MaHang)
                .Where(c => c.IsActive == true)
                .ToPagedList(page, pageSize);
           
            return View(model);
        }
        public IActionResult ThoiTrang(int page = 1, int pageSize = 10)
        {
           

            var model = _context.Hangs.OrderByDescending(k => k.MaHang)
                .Where(c => c.Src == "Ava" && c.IsActive == true)
                .ToPagedList(page, pageSize);

            return View(model);


        }
        public IActionResult Vukhi(int page = 1, int pageSize = 10)
        {
            

            var model = _context.Hangs.OrderByDescending(k => k.MaHang)
                .Where(c => c.Src == "Weapon" && c.IsActive == true)
                .ToPagedList(page, pageSize);

            return View(model);


        }
        public IActionResult Giap(int page = 1, int pageSize = 10)
        {
          

            var model = _context.Hangs.OrderByDescending(k => k.MaHang)
                .Where(c => c.Src == "Protector" && c.IsActive == true)
                .ToPagedList(page, pageSize);

            return View(model);


        }
        public IActionResult TrangSuc(int page = 1, int pageSize = 10)
        {
       

            var model = _context.Hangs.OrderByDescending(k => k.MaHang)
                .Where(c => c.Src == "Accessory" && c.IsActive == true)
                .ToPagedList(page, pageSize);
            return View(model);


        }

        public IActionResult _partialHang()
        {
            return PartialView();
        }

        public IActionResult _partialDanhSachHang()
        {
            return PartialView();
        }
        public IActionResult _partialKhuyenMai()
        {
            return PartialView();
        }
        public IActionResult _partialChitiethang()
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