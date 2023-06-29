using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Super.Models;
using X.PagedList;

namespace Super.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("nhanhieu")]
    public class NhanHieuController : Controller
    {
        private readonly QlbhContext db;
        public NhanHieuController(QlbhContext context)
        {
            db = context;
        }
        [Route("index")]
        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            var model = db.NhanHieus.OrderByDescending(x => x.MaNhanHieu).ToPagedList(page, pageSize);
            return View(model);
        }
        [Route("xoa")]
        public IActionResult Xoa(string? iden, bool isActive)
        {
            var itemToRemove = db.NhanHieus.FirstOrDefault(x => x.MaNhanHieu == iden); //returns a single item.

            if (itemToRemove != null)
            {
                itemToRemove.IsActive = !isActive;
                db.Update(itemToRemove);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "NhanHieu", new { area = "Admin" });

            // Update
        }
        [Route("them")]
        public IActionResult Them(string? manhanhieu, string? tennhanhieu, string? macungcap, bool? trangthai)
        {
            if (!String.IsNullOrEmpty(manhanhieu))
            {
                NhanHieu kmdb = new NhanHieu();

                kmdb.MaNhanHieu = manhanhieu;
                kmdb.TenNhanHieu = tennhanhieu;
                kmdb.MaCungCap = macungcap;
                kmdb.Filter = manhanhieu.ToLower() + " " + tennhanhieu.ToLower() + " " + macungcap.ToLower();
                kmdb.IsActive = trangthai;


                //hang.Filter = tenhang.ToLower() + manhanhieu.ToLower() + A.ghg(tenhang.ToLower());
                db.Add(kmdb);
                db.SaveChanges();
                return RedirectToAction("Index", "NhanHieu", new { area = "Admin" });
            }
            return View();

        }
        [Route("cap-nhat")]
        public IActionResult CapNhat(string? iden, string? tennhanhieu, string? macungcap, bool? trangthai, bool? isUpdate = true)
        {

            var itemToUpdate = db.NhanHieus.FirstOrDefault(x => x.MaNhanHieu == iden);

            if (!(bool)isUpdate)
            {
                return View(itemToUpdate);
            }
            else
            {
                itemToUpdate.MaNhanHieu = iden;
                itemToUpdate.TenNhanHieu = tennhanhieu;
                itemToUpdate.MaCungCap = macungcap;
                itemToUpdate.Filter = iden.ToLower() + " " + tennhanhieu.ToLower() + " " + macungcap.ToLower();
                db.Update(itemToUpdate);
                db.SaveChanges();
                return RedirectToAction("Index", "NhanHieu", new { area = "Admin" });
            }
        }
    }
}
