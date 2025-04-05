using DataAccessLayer.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Model
{
    public class Schedules
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public WeekDay DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        
        public int SubjectId { get; set; }

    }
}
