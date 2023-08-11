using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Super.Models;
using Newtonsoft.Json;

namespace Super.Controllers
{
    public class GioHangController : Controller
    {
        private readonly QlbhContext _context;

        public GioHangController(QlbhContext context)
        {
            _context = context;
        }
        public IActionResult Cart()
        {
            // lấy dữ liệu của cookie về
            List<GioHang> GioHang = GetListFromCookie();
            // lọc lấy danh sách productId trong cookie để query xuống database
            List<int> productIDs = GioHang.Select(i => i.MaHang).ToList();

            var products = _context.Hangs.Where(p => productIDs.Contains(p.MaHang)).ToList();

            var cartProducts = GioHang.Join(products,
                GioHang => GioHang.MaHang,
                products => products.MaHang,
                (GioHang, products) => new DanhSachGioHang
                {
                    MaHang = GioHang.MaHang,
                    TenHang = products.TenHang,
                    SoLuong = GioHang.SoLuong,
                    DonGia = products.DonGia,
                    HinhAnh = products.HinhAnh
                }).ToList();

            return View(cartProducts);
        }


        [HttpPost]
        // xử lí action thêm sản phẩm vào giỏ hàng (thêm sản phẩm vào cookie)
        public IActionResult AddToCart(int ProductId)
        {
            List<GioHang> gioHang = GetListFromCookie();

            GioHang existItem = gioHang.Find(x => x.MaHang == ProductId);
            if (existItem != null)
            {
                existItem.SoLuong += 1;
            }
            else
            {
                Hang product = _context.Hangs.FirstOrDefault(p => p.MaHang == ProductId);
                GioHang giohang = new GioHang()
                {
                    MaHang = ProductId,
                    SoLuong = 1,
                    DonGia = product.DonGia
                };
                gioHang.Add(giohang);
            }
            SaveListToCookie(gioHang);
            return Ok();
        }



        // xử lý lấy danh sách sản phẩm từ cookie
        private List<GioHang> GetListFromCookie()
        {
            // check xem da co CartList trong cookie hay chua
            if (Request.Cookies.ContainsKey("GioHang"))
            {
                // nếu có lấy ra từ cookie chuyển thành json và lưu vào List và trả về cho action addtocart
                string CartItemJson = Request.Cookies["GioHang"];
                return JsonConvert.DeserializeObject<List<GioHang>>(CartItemJson);

            }
            else
            {
                return new List<GioHang>();

            }
        }

        // xử lý luu danh sách sản phẩm vào cookie
        private void SaveListToCookie(List<GioHang> gioHang)
        {
            string cartlistJson = JsonConvert.SerializeObject(gioHang);

            // save vào cookie

            Response.Cookies.Append("GioHang", cartlistJson, new CookieOptions
            {
                Expires = DateTime.Now.AddDays(100),
                SameSite = SameSiteMode.Strict
            });
        }

        // Xóa sản phẩm trong Cart

        [HttpPost]

        public IActionResult DeleteCart(int productId)
        {
            List<GioHang> cartList = GetListFromCookie();
            GioHang ItemToremove = cartList.FirstOrDefault(x => x.MaHang == productId);

            if (ItemToremove != null)
            {
                cartList.Remove(ItemToremove);
                SaveListToCookie(cartList);
            }
            return RedirectToAction("Cart");
        }
    }
}
