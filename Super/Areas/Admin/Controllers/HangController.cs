using Microsoft.AspNetCore.Mvc;

namespace Super.Areas.Admin.Controllers
{
    public class HangController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
        [Area("Admin")]
        public IActionResult _partialDanhSachHang()
        {
            return PartialView();
        }
    }
}
