using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using Super.Models;

namespace Super.Controllers
{
    public class HangController : Controller
    {
        private readonly QlbhContext _context;
        public HangController(QlbhContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var item = _context.Hangs.ToList();
            return View(item);
        }
        public IActionResult Xoa(int? mahang)
        {
            var itemToRemove = _context.Hangs.FirstOrDefault(x => x.MaHang == mahang); //returns a single item.

            if (itemToRemove != null)
            {
                _context.Hangs.Remove(itemToRemove);
                _context.SaveChanges();
            }
            return Redirect("/Home/Index");

            // Update
        }
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
                return Redirect("/Home/Index");
            }
        }
        public IActionResult Them(int? mahang, string? tenhang, string? dongiaban,string? manhanhieu, string? hinhanh)
        {
            if (!String.IsNullOrEmpty(tenhang))
            {
                Hang hang = new Hang();
                
                hang.TenHang = tenhang;
                hang.DonGiaHang = dongiaban;
                hang.MaNhanHieu = manhanhieu;
                hang.HinhAnh = hinhanh;
                
               
                _context.Add(hang);
                _context.SaveChanges();
                return Redirect("/Home/Index/");
            }
            else
            {
                Hang hang = new Hang();
                return View(hang);
            }

        }
        public IActionResult Search(string searchData)
        {

            if (!String.IsNullOrEmpty(searchData))
            {
                var searchResults = _context.Hangs
                //.Where(x => EF.Functions.Like(x.Filter, "%" + searchData + "%"))
                .Where(x => x.Filter.Contains(searchData))
                .ToList();
                return Json(searchResults);
            }
            else
            {
                return View();
            }

        }


    }
}
