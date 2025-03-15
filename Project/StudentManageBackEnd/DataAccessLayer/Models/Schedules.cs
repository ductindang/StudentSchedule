using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class Schedules
    {
        [Key]
        public int Id {  get; set; }
        public int UserId { get; set; }
        public int SubjectId { get; set; }
        public DateTime StudyDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public Subjects Subjects { get; set; }
    }
}
