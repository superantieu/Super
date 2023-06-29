using Microsoft.AspNetCore.Mvc;
using Super.Models;
using X.PagedList;

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
        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            //var supe = new Sup();
            //var model = supe.ListAllPaging(page, pageSize);
            var model = _context.Hangs.OrderByDescending(x => x.MaHang).ToPagedList(page, pageSize);
            //var item = _context.Hangs.ToList();
            return View(model);
        }
        [Route("xoa")]
        public IActionResult Xoa(int? mahang, bool isActive)
        {
            var itemToRemove = _context.Hangs.FirstOrDefault(x => x.MaHang == mahang); //returns a single item.

            if (itemToRemove != null)
            {
                itemToRemove.IsActive = !isActive;
                _context.Update(itemToRemove);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Hang", new { area = "Admin" });

            // Update
        }
        [Route("them")]
        public IActionResult Them(int? mahang, string? tenhang, string? dongiaban, string? manhanhieu,string? mota, string? chungloai,string? khuyenmai, string? imageurl ,string? hinhanh, bool? trangthai)
        {
            if (!String.IsNullOrEmpty(tenhang))
            {
                Hang hang = new Hang();

                hang.TenHang = tenhang;
                hang.DonGiaHang = dongiaban;
                hang.MaNhanHieu = manhanhieu;
                hang.HinhAnh = imageurl;
                hang.MoTa = mota;
                hang.Src = chungloai;
                hang.Url = khuyenmai;
                hang.IsActive = trangthai;


                //hang.Filter = tenhang.ToLower() + manhanhieu.ToLower() + A.ghg(tenhang.ToLower());
                _context.Add(hang);
                _context.SaveChanges();
                return RedirectToAction("Index", "Hang", new { area = "Admin" });
            }
            return View();

        }
        [Route("cap-nhat")]
        public IActionResult CapNhat(int? mahang, string? tenhang, string? dongiaban, string? manhanhieu, string? mota, string? chungloai, string? khuyenmai, string? imageurl, string? hinhanh, bool? isUpdate = true)
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
                itemToUpdate.HinhAnh = imageurl;
                itemToUpdate.MoTa = mota;
                itemToUpdate.Src = chungloai;
                itemToUpdate.Url = khuyenmai;
                _context.Update(itemToUpdate);
                _context.SaveChanges();
                return RedirectToAction("Index", "Hang", new { area = "Admin" });
            }
        }

    }
}
