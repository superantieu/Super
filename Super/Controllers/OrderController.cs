using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Super.Models;

namespace Super.Controllers
{
    public class OrderController : Controller
    {
        private readonly QlbhContext _context;

        public OrderController(QlbhContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }


        // tạo Action Order

        public IActionResult CreateOrder()
        {
            // lấy userId từ session
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));

            // lấy danh sách sản phẩm từ cookies

            List<GioHang> gioHangs = GetListFromCookie();

            // tính tổng Total Price

            decimal? totalPrice = gioHangs.Sum(i => i.SoLuong * i.DonGia);

            HoaDon order = new HoaDon()
            {
                MaKh = userId,
                TongTien = totalPrice,
                NgayLap = DateTime.Now,

            };

            _context.HoaDons.Add(order);
            _context.SaveChanges();





            // lưu thông tin chi tiết vào bảng OrderItem
            foreach (var item in gioHangs)
            {
                ChiTietHoaDon orderItem = new ChiTietHoaDon()
                {
                    //OrderItem1 = GenerateRandomOrderId(),
                    OrderId = order.MaHd,
                    ProductId = item.MaHang,
                    SoLuongMua = item.SoLuong,
                    DonGiaBan = item.DonGia,
                    ThanhTien = item.SoLuong * item.DonGia,
                };


                _context.Entry(orderItem).State = EntityState.Added;
                _context.SaveChanges();
            }



            ClearCookie();

            return RedirectToAction("OrderConfirm", new { OrderId = order.MaHd });

        }

        // lấy thông tin trả về view order
        public IActionResult OrderConfirm(int orderId)
        {
            // lấy thông tin đơn hàng từ db

            HoaDon order = _context.HoaDons.FirstOrDefault(i => i.MaHd == orderId);

            // lấy thông tin sản phẩm trong đơn hàng 
            List<ChiTietHoaDon> orderItems = _context.ChiTietHoaDons.Where(i => i.OrderId == orderId).ToList();

            return View(new XacNhanOrder
            {
                HoaDon = order,
                ChiTietHoaDons = orderItems
            });

        }





        // Xóa đi cookie

        private void ClearCookie()
        {
            Response.Cookies.Delete("GioHang");
        }

        // get list form cookie

        private List<GioHang> GetListFromCookie()
        {
            if (Request.Cookies.ContainsKey("GioHang"))
            {
                string cartItemJson = Request.Cookies["GioHang"];
                return JsonConvert.DeserializeObject<List<GioHang>>(cartItemJson);
            }
            else
            {
                return new List<GioHang>();
            }
        }

        public int GenerateRandomOrderId()
        {
            Random random = new Random();
            int orderId = random.Next(1, 999999);
            return orderId;
        }

    }
}
