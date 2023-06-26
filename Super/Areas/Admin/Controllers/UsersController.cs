using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Super.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Super.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("users")]
    public class UsersController : Controller
    {
        private readonly QlbhContext db;
        public UsersController(QlbhContext context)
        {
            db = context;
        }
        [Route("index")]
        public IActionResult Index()
        {
            var use = new User();

            return View(use);
        }
        [Route("login")]


       
        public ActionResult Login(string email, string password)
        {
             
            // Kiểm tra thông tin đăng nhập
            var user = db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                // Đăng nhập thành công
                // Lưu thông tin người dùng vào session
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("UserName", user.UserName);
                HttpContext.Session.SetInt32("RoleId", (int)user.RoleId);
                // Chuyển hướng người dùng tùy thuộc vào vai trò
                if (user.RoleId == 1)
                {
                    // Chuyển đến trang quản lý của admin
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else if (user.RoleId == 2)
                {
                    // Chuyển đến trang mua hàng của người dùng
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
            }
            else if (email != null || password != null) { ViewBag.Message = "Email hoặc mật khẩu không đúng"; }
            else
            {
                // Đăng nhập thất bại
                 ViewBag.Message = "";
                
               
            }
            return View();
        }
    }
}
