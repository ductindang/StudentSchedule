using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManageFrontEnd.Models
{
    public class Subject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên môn học không được bỏ trống")]
        [RegularExpression(@"^[^\s].*[^\s]$", ErrorMessage = "Tên môn học không được chứa khoảng trắng ở đầu và cuối.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Mô tả không được bỏ trống")]
        [RegularExpression(@"^[^\s].*[^\s]$", ErrorMessage = "Mô tả không được chứa khoảng trắng ở đầu và cuối.")]
        public string Description { get; set; }
        public int UserId { get; set; }
        //public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
