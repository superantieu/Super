using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Super.Models;
using X.PagedList;

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
        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            var model = db.Users.OrderByDescending(x => x.UserId).ToPagedList(page, pageSize);
            return View(model);
        }
        [Route("login")]
        public ActionResult Login(string email, string password)
        {

            // Kiểm tra thông tin đăng nhập
            var user = db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null && user.IsActive == false)
            {
                ViewBag.Message = "Tài khoản đã bị khóa vì chửi Admin";
            }
            else if (user != null)
            {
                // Đăng nhập thành công
                // Lưu thông tin người dùng vào session
                //HttpContext.Session.SetInt32("UserId", user.UserId);
                //HttpContext.Session.SetString("UserName", user.UserName);
                //HttpContext.Session.SetInt32("RoleId", (int)user.RoleId);
                // Chuyển hướng người dùng tùy thuộc vào vai trò
                if (user.RoleId == 1)
                {
                    if (HttpContext.Session.GetString("AdminUserId") == null)
                    {
                        HttpContext.Session.SetString("AdminUserId", user.UserId.ToString());
                    }
                    // Chuyển đến trang quản lý của admin
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else if (user.RoleId == 2)
                {
                    if (HttpContext.Session.GetString("UserId") == null)
                    {
                        HttpContext.Session.SetString("UserId", user.UserId.ToString());
                    }
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
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            Response.Cookies.Delete("GioHang");

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [Route("xoa")]
        public IActionResult Xoa(int? iden, bool isActive)
        {
            var itemToRemove = db.Users.FirstOrDefault(x => x.UserId == iden); //returns a single item.

            if (itemToRemove != null)
            {
                itemToRemove.IsActive = !isActive;
                db.Update(itemToRemove);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Users", new { area = "Admin" });

            // Update
        }

        [Route("them")]
        public IActionResult Them(int? userid, string? username, int? roleid, string? email, string? password, int makh, bool? trangthai)
        {
            if (!String.IsNullOrEmpty(username))
            {
                User kmdb = new User();

                kmdb.UserName = username;
                kmdb.RoleId = roleid;
                kmdb.Email = email;
                kmdb.Password = password;
                kmdb.MaKh = makh;
                kmdb.Filter = username.ToLower() + " " + email.ToLower() + makh;
                kmdb.IsActive = trangthai;


                //hang.Filter = tenhang.ToLower() + manhanhieu.ToLower() + A.ghg(tenhang.ToLower());
                db.Add(kmdb);
                db.SaveChanges();
                return RedirectToAction("Index", "Users", new { area = "Admin" });
            }
            return View();

        }
        [Route("cap-nhat")]
        public IActionResult CapNhat(int? iden, string? username, int? roleid, string? email, string? password, int makh, bool? trangthai, bool? isUpdate = true)
        {

            var itemToUpdate = db.Users.FirstOrDefault(x => x.UserId == iden);

            if (!(bool)isUpdate)
            {
                return View(itemToUpdate);
            }
            else
            {
                itemToUpdate.UserName = username;
                itemToUpdate.RoleId = roleid;
                itemToUpdate.Email = email;
                itemToUpdate.Password = password;
                itemToUpdate.MaKh = makh;
                itemToUpdate.Filter = username.ToLower() + " " + email.ToLower() + makh;
                db.Update(itemToUpdate);
                db.SaveChanges();
                return RedirectToAction("Index", "Users", new { area = "Admin" });
            }
        }
        [Route("signup")]
        public IActionResult SignUp(int? userid, string? username, int? roleid, string? email, string? password, int makh, bool? trangthai)
        {
            if (!String.IsNullOrEmpty(username))
            {
                User sg = new User();

                sg.UserName = username;
                sg.RoleId = 2;
                sg.Email = email;
                sg.Password = password;
                sg.MaKh = makh;
                sg.Filter = username.ToLower() + " " + email.ToLower() + makh;
                sg.IsActive = trangthai;


                //hang.Filter = tenhang.ToLower() + manhanhieu.ToLower() + A.ghg(tenhang.ToLower());
                db.Add(sg);
                db.SaveChanges();
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            return View();

        }

    }
}
