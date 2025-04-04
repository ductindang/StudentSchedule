using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManageFrontEnd.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        //public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
