using Microsoft.AspNetCore.Mvc;
using Super.Models;

namespace Super.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("hang")]
    public class HangController : Controller
    {
        private readonly QlbhContext _context;
        public HangController(QlbhContext context)
        {
            _context = context;
        }

        [Route("index")]
        public IActionResult Index()
        {
            var item = _context.Hangs.ToList();
            return View(item);
        }
        [Route("xoa")]
        public IActionResult Xoa(int? mahang)
        {
            var itemToRemove = _context.Hangs.FirstOrDefault(x => x.MaHang == mahang); //returns a single item.

            if (itemToRemove != null)
            {
                _context.Hangs.Remove(itemToRemove);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Hang", new { area = "Admin" });

            // Update
        }
        [Route("them")]
        public IActionResult Them(int? mahang, string? tenhang, string? dongiaban, string? manhanhieu, string? hinhanh, bool? kichhoat)
        {
            if (!String.IsNullOrEmpty(tenhang))
            {
                Hang hang = new Hang();

                hang.TenHang = tenhang;
                hang.DonGiaHang = dongiaban;
                hang.MaNhanHieu = manhanhieu;
                hang.HinhAnh = hinhanh;
                //hang.Filter = tenhang.ToLower() + manhanhieu.ToLower() + A.ghg(tenhang.ToLower());
                _context.Add(hang);
                _context.SaveChanges();
                return RedirectToAction("Index", "Hang", new { area = "Admin" });
            }
            return View();

        }
        [Route("cap-nhat")]
        public IActionResult CapNhat(int? mahang, string? tenhang, string? dongiaban, string? manhanhieu, string? hinhanh, bool? isUpdate = true)
        {

            var itemToUpdate = _context.Hangs.FirstOrDefault(x => x.MaHang == mahang);

            if (!(bool)isUpdate)
            {
                return View(itemToUpdate);
            }
            else
            {
                itemToUpdate.TenHang = tenhang;
                itemToUpdate.DonGiaHang = dongiaban;
                itemToUpdate.MaNhanHieu = manhanhieu;
                itemToUpdate.HinhAnh = hinhanh;
                _context.Update(itemToUpdate);
                _context.SaveChanges();
                return RedirectToAction("Index", "Hang", new { area = "Admin" });
            }
        }

    }
}
