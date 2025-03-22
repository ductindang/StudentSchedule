using BusinessLogicLayer.Helper;
using BusinessLogicLayer.ServicesInterface;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace StudentManageBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetAllUser()
        {
            var users = await _userService.GetAllUser();
            return Ok(users);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("EmailPassword")]
        public async Task<ActionResult<Users>> GetUserByEmailPassword(string email, string password)
        {
            var user = await _userService.GetUserByEmailPassword(email, password);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<Users>> InsertUser(Users newUser)
        {
            var user = await _userService.InsertUser(newUser);
            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult<Users>> UpdateUser([FromBody] Users newUser, int id)
        //{
        //    var user = await _userService.GetUserById(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    newUser.Id = id;
        //    user = newUser;
        //    await _userService.UpdateUser(user);

        //    return Ok(user);
        //}

        [Authorize]
        [HttpPut("update")]
        public async Task<ActionResult<Users>> UpdateUser([FromBody] Users updatedUser)
        {
            // Lấy ID của người dùng từ token
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized(new { message = "Không xác thực được người dùng!" });
            }

            // Lấy thông tin người dùng từ ID trong token
            var user = await _userService.GetUserById(userId.Value);
            if (user == null)
            {
                return NotFound(new { message = "Người dùng không tồn tại!" });
            }

            // Chỉ cập nhật các trường cần thiết (không ghi đè ID)
            user.FullName = updatedUser.FullName ?? user.FullName;
            user.Email = updatedUser.Email ?? user.Email;
            user.Password = updatedUser.Password ?? user.Password;

            // Cập nhật thông tin
            await _userService.UpdateUser(user);

            return Ok(user);
        }

        // Hàm lấy UserID từ token
        private int? GetUserIdFromToken()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                var userIdClaim = userClaims.FirstOrDefault(c => c.Type == "UserId");
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    return userId;
                }
            }
            return null;
        }


        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUser(int id)
        //{
        //    var user = await _userService.DeleteUser(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(user);
        //}

        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser()
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized(new { message = "Không xác thực được người dùng!" });
            }

            // Lấy thông tin người dùng từ ID trong token
            var user = await _userService.GetUserById(userId.Value);
            if (user == null)
            {
                return NotFound(new { message = "Người dùng không tồn tại!" });
            }

            // Cập nhật thông tin
            await _userService.DeleteUser(user.Id);

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var token = await _userService.Login(model.Email, model.Password);
            if (token == null)
                return Unauthorized(new { message = "Email hoặc mật khẩu không đúng!" });

            return Ok(new { Token = token });
        }


    }
}
