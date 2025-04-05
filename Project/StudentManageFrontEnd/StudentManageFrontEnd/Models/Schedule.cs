using StudentManageFrontEnd.Models.Enum;
using StudentManageFrontEnd.Services;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace StudentManageFrontEnd.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SubjectId { get; set; }
        [AllowNull]
        public string SubjectName { get; set; }
        public WeekDay DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        //public virtual Subject Subjects { get; set; }
    }
}
