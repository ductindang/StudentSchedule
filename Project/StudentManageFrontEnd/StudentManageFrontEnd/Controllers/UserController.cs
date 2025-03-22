using Microsoft.AspNetCore.Mvc;
using StudentManageFrontEnd.Models;
using StudentManageFrontEnd.Services.IServices;

namespace StudentManageFrontEnd.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Login(User user)
        //{
        //    var email = user.Email;
        //    var password = user.Password;
        //    var result = _userService.GetUserByEmailPassword(email, password);
        //    if (result == null)
        //    {
        //        ViewBag.Error = "Wrong email or password !!! Please, fill in this box";
        //        return View();
        //    }
        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return View();
            }

            var token = await _userService.Login(user.Email, user.Password); // Gọi bất đồng bộ

            if (string.IsNullOrEmpty(token))
            {
                ViewBag.Error = "Wrong email or password! Please try again.";
                return View();
            }

            // Lưu token vào session (tuỳ vào cách bạn muốn lưu)
            HttpContext.Session.SetString("AuthToken", token);

            return RedirectToAction("Index", "Home"); // Chuyển hướng sau khi đăng nhập thành công
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Xóa tất cả dữ liệu trong session
            return RedirectToAction("Index", "Home"); // Chuyển về trang chủ
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User model)
        {
            if (ModelState.IsValid)
            {
                var newUser = await _userService.InsertUser(model);
                if (newUser == null)
                {
                    ViewBag.Error = "Registration failed. Email might already be in use.";
                    return View(model);
                }
                return RedirectToAction("Login");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete()
        {
            string token = HttpContext.Session.GetString("AuthToken");

            if (token != null)
            {
                var result = await _userService.DeleteUser(token);
            }

            return Logout();
        }

    }
}
