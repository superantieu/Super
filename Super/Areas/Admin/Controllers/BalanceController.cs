using Microsoft.AspNetCore.Mvc;
using Super.Models;
using X.PagedList;

namespace Super.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("khuyenmai")]
    public class BalanceController : Controller
    {
        private readonly QlbhContext _context;
        public BalanceController(QlbhContext context)
        {
            _context = context;
        }
        [Route("index")]
        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            var model = _context.Balances.OrderByDescending(x => x.Url).ToPagedList(page, pageSize);
            return View(model);
        }
        [Route("xoa")]
        public IActionResult Xoa(string? makm, bool isActive)
        {
            var itemToRemove = _context.Balances.FirstOrDefault(x => x.Url == makm); //returns a single item.

            if (itemToRemove != null)
            {
                itemToRemove.IsActive = !isActive;
                _context.Update(itemToRemove);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Balance", new { area = "Admin" });

            // Update
        }
        [Route("them")]
        public IActionResult Them(string? makm, string? khuyenmai, string? moigoi, bool? trangthai)
        {
            if (!String.IsNullOrEmpty(makm))
            {
                Balance kmdb = new Balance();

                kmdb.Url = makm;
                kmdb.Km = khuyenmai;
                kmdb.Details = moigoi;
                kmdb.IsActive = trangthai;


                //hang.Filter = tenhang.ToLower() + manhanhieu.ToLower() + A.ghg(tenhang.ToLower());
                _context.Add(kmdb);
                _context.SaveChanges();
                return RedirectToAction("Index", "Balance", new { area = "Admin" });
            }
            return View();

        }
        [Route("cap-nhat")]
        public IActionResult CapNhat(string? makm, string? khuyenmai, string? moigoi, bool? isUpdate = true)
        {

            var itemToUpdate = _context.Balances.FirstOrDefault(x => x.Url == makm);

            if (!(bool)isUpdate)
            {
                return View(itemToUpdate);
            }
            else
            {
                itemToUpdate.Url = makm;
                itemToUpdate.Km = khuyenmai;
                itemToUpdate.Details = moigoi;
                _context.Update(itemToUpdate);
                _context.SaveChanges();
                return RedirectToAction("Index", "Balance", new { area = "Admin" });
            }
        }
    }
}