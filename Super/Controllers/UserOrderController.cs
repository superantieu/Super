using Microsoft.AspNetCore.Mvc;
using Super.Models;

namespace Super.Controllers
{
    public class UserOrderController : Controller
    {
        private readonly QlbhContext _context;
        public UserOrderController(QlbhContext context)
        {
            _context = context;
        }

        public IActionResult GetOrder()
        {
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));

            var OrderItem = _context.HoaDons.Where(o => o.MaKh == userId).ToList();

            return View(OrderItem);
        }

        public IActionResult OrderDetail(int orderId)
        {
            // lấy thông tin đơn hàng từ db

            var Orderdetail = _context.ChiTietHoaDons.Where(o => o.MaHd == orderId).ToList();


            return View(Orderdetail);

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
