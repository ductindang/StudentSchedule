using System.ComponentModel.DataAnnotations;

namespace StudentManageFrontEnd.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Họ tên không được để trống")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password không được để trống.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password chứa ít nhất 8 ký tự.")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Password không được chứa ký tự khoảng cách.")]
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public string Token { get; set; }
    }
}
