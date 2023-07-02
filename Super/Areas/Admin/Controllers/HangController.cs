using Microsoft.AspNetCore.Mvc;
using Super.Models;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using Super.Areas.Admin.Models;

namespace Super.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("hang")]
    public class HangController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly QlbhContext _context;
        public HangController(QlbhContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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
        [Route("moi")]
        public IActionResult New(int page = 1, int pageSize = 10)
        {
            //var supe = new Sup();
            //var model = supe.ListAllPaging(page, pageSize);
            var model = _context.Hangs.OrderByDescending(x => x.NgayNhap).ToPagedList(page, pageSize);
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
       
        [HttpGet]
        public IActionResult Them()
        {
            return View();
        }
        [HttpPost]
        
        public IActionResult Them(Hang model, IFormFile hinhanh)
        {
           
                try
                {
                    var hang = new Hang

                    {
                     //   hang.TenHang = tenhang;
                    //        hang.DonGiaHang = dongiaban;
                    //        hang.MaNhanHieu = manhanhieu;
                    //        hang.MoTa = mota;
                    //        hang.Src = chungloai;
                    //        hang.Url = khuyenmai;
                    //        hang.NgayNhap = ngaynhap;
                    //        hang.IsActive = trangthai;
                    TenHang = model.TenHang,
                        DonGiaHang = model.DonGiaHang,
                        MaNhanHieu = model.MaNhanHieu,
                        MoTa = model.MoTa,
                        Src = model.Src,
                        Url = model.Url,
                        NgayNhap = model.NgayNhap,
                        IsActive = model.IsActive,

                    };

                    var entry = _context.Entry(hang);
                    entry.State = EntityState.Added;
                    entry.Property("Filter").IsModified = false;
                    _context.SaveChanges();

                    // kiểm tra xe file ảnh có bị nul hay là không
                    if (hinhanh != null && hinhanh.Length > 0)
                    {

                        // tạo đường dẫn dến thư mục lưu ảnh
                        var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "asset/image", hinhanh.FileName);

                        // tạo luồng để lưu ảnh vào đường dẫn
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            hinhanh.CopyTo(stream);
                        }
                        // lưu đường dẫn vào database
                        hang.HinhAnh = "/asset/image/" + hinhanh.FileName;
                        _context.SaveChanges();
                    }




                    return RedirectToAction("Index", "Hang", new { area = "Admin" });
                }
                catch (Exception ex)
                {

                }
            
            return View(model);
        }
        //[Route("them")]
        //public IActionResult Them(int? mahang, string? tenhang, string? dongiaban, string? manhanhieu,string? mota, string? chungloai,string? khuyenmai,DateTime? ngaynhap, string? imageurl , IFormFile hinhanh, bool? trangthai)
        //{
        //    if (!String.IsNullOrEmpty(tenhang))
        //    {
        //        Hang hang = new Hang();

        //        hang.TenHang = tenhang;
        //        hang.DonGiaHang = dongiaban;
        //        hang.MaNhanHieu = manhanhieu;
        //        hang.MoTa = mota;
        //        hang.Src = chungloai;
        //        hang.Url = khuyenmai;
        //        hang.NgayNhap = ngaynhap;
        //        hang.IsActive = trangthai;


        //        //hang.Filter = tenhang.ToLower() + manhanhieu.ToLower() + A.ghg(tenhang.ToLower());
        //        _context.Add(hang);
        //        _context.SaveChanges();
        //        if (hinhanh != null && hinhanh.Length > 0)
        //        {

        //            // tạo đường dẫn dến thư mục lưu ảnh
        //            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "asset/image", hinhanh.FileName);

        //            // tạo luồng để lưu ảnh vào đường dẫn
        //            using (var stream = new FileStream(imagePath, FileMode.Create))
        //            {
        //                hinhanh.CopyTo(stream);
        //            }
        //            // lưu đường dẫn vào database
        //            hang.HinhAnh = "/asset/image/" + hinhanh.FileName;
        //            _context.SaveChanges();
        //        }
        //        return RedirectToAction("Index", "Hang", new { area = "Admin" });
        //    }
        //    return View();

        //}
        [Route("cap-nhat")]
        public IActionResult CapNhat(int? mahang, string? tenhang, string? dongiaban, string? manhanhieu, string? mota, string? chungloai, string? khuyenmai, DateTime? ngayban, IFormFile hinhanh, bool? isUpdate = true)
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
                
                itemToUpdate.MoTa = mota;
                itemToUpdate.Src = chungloai;
                itemToUpdate.Url = khuyenmai;
                itemToUpdate.NgayNhap = ngayban;
                _context.Update(itemToUpdate);
                _context.SaveChanges();
                if (hinhanh != null && hinhanh.Length > 0)
                {

                    // tạo đường dẫn dến thư mục lưu ảnh
                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "asset/image", hinhanh.FileName);

                    // tạo luồng để lưu ảnh vào đường dẫn
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        hinhanh.CopyTo(stream);
                    }
                    // lưu đường dẫn vào database
                    itemToUpdate.HinhAnh = "/asset/image/" + hinhanh.FileName;
                    _context.SaveChanges();
                }
                return RedirectToAction("Index", "Hang", new { area = "Admin" });
            }
        }
        [Route("search")]
        public IActionResult Search(string searchData)
        {

            if (!String.IsNullOrEmpty(searchData))
            {
                var searchResults = _context.Hangs

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
