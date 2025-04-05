using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;
using StudentManageFrontEnd.Models;
using StudentManageFrontEnd.Services;
using StudentManageFrontEnd.Services.IServices;

namespace StudentManageFrontEnd.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private string _token;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            _token = HttpContext.Session.GetString("AuthToken") ?? string.Empty;
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

            return RedirectToAction("Index", "Schedule"); // Chuyển hướng sau khi đăng nhập thành công
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Xóa tất cả dữ liệu trong session
            return RedirectToAction("Index", "Schedule"); // Chuyển về trang chủ
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

        [HttpGet]
        public async Task<IActionResult> Update()
        {
            var user = await _userService.GetUserData(_token);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var newUser = await _userService.UpdateUser(user, _token);
            if (newUser == null)
            {
                TempData["ErrorMessage"] = "Sửa người dùng thất bại";
                return View(newUser);
            }

            return RedirectToAction("Index", "Schedule");
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
