using StudentManageFrontEnd.Models;

namespace StudentManageFrontEnd.Services.IServices
{
    public interface IScheduleService
    {
        public Task<IEnumerable<Schedule>> GetScheduleByUserId(string token);
        public Task<Schedule> GetScheduleById(int id, string token);
        public Task<Schedule> InsertSchedule(Schedule schedule, string token);
        public Task<Schedule> UpdateSchedule(Schedule schedule, string token);
        public Task<Schedule> DeleteSchedule(int id, string token);
    }
}
