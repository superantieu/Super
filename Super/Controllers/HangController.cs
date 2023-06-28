using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using Super.Areas.Admin.Models;
using Super.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Super.Controllers
{
    public class HangController : Controller
    {
        private readonly QlbhContext _context;
        public HangController(QlbhContext context)
        {
            _context = context;
        }
      
        public IActionResult innerIndex(int? mahang, string? tenhang, string? dongiaban, string? manhanhieu,string? url, string? hinhanh)
        
        {
       

            // return View(data);


            var itemToUpdate = _context.Hangs.FirstOrDefault(x => x.MaHang == mahang);
           

            return View(itemToUpdate);
        }
     
           public IActionResult Super(int? mahang, string? tenhang, string? dongiaban, string? manhanhieu,string? url, string? hinhanh)
        
        {
            HomeData data = new HomeData();
             
            var itema = _context.Balances.OrderByDescending(k => k.Url).Where(c => c.Url == url).ToList();
            var item = _context.Hangs.OrderByDescending(k => k.MaHang).Where(c => c.MaHang == mahang).ToList();
            data.KMDB = itema;
            data.DSH = item;


            // return View(data);


            
           

            return View(data);
        }
     

     
        public IActionResult _partialKhuyenMai()
        {
            
            return PartialView();
        }
        public IActionResult _partialChiTietHang()
        {
            return PartialView();
        }


        public IActionResult Them(int? mahang, string? tenhang, string? dongiaban,string? manhanhieu, string? hinhanh, bool? kichhoat)
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
            return View();

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
