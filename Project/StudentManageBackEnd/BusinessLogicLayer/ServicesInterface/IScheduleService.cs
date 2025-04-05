using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.ServicesInterface
{
    public interface IScheduleService
    {
        public Task<IEnumerable<Schedules>> GetAllSchedules();
        public Task<IEnumerable<Schedules>> GetSchedulesByUser(int userId);
        public Task<Schedules> GetScheduleById(int id);
        public Task<Schedules> InsertSchedule(Schedules schedule);
        public Task<Schedules> UpdateSchedule(Schedules schedule);
        public Task<Schedules> DeleteSchedule(int id);
        public Task<IEnumerable<Schedules>> DeleteSchedulesInUser(int userId);
    }
}
