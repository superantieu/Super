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
      
            var model = _context.Hangs.OrderByDescending(x => x.MaHang)
                .Where(c => c.IsActive == true)
                .ToPagedList(page, pageSize);
           
            return View(model);
        }
        public IActionResult Name(int page = 1, int pageSize = 10)
        {

            var model = _context.Hangs.OrderBy(x => x.TenHang)
                .Where(c => c.IsActive == true)
                .ToPagedList(page, pageSize);

            return View(model);
        }
        public IActionResult New(int page = 1, int pageSize = 10)
        {

            var model = _context.Hangs.OrderByDescending(x => x.NgayNhap)
                .Where(c => c.IsActive == true)
                .ToPagedList(page, pageSize);

            return View(model);
        }
        public IActionResult TopPrice(int page = 1, int pageSize = 10)
        {

            var model = _context.Hangs.OrderByDescending(x => x.DonGiaHang)
                .Where(c => c.IsActive == true)
                .ToPagedList(page, pageSize);

            return View(model);
        }
        public IActionResult BotPrice(int page = 1, int pageSize = 10)
        {

            var model = _context.Hangs.OrderBy(x => x.DonGiaHang)
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