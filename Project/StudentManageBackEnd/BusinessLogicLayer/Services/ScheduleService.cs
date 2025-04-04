using BusinessLogicLayer.ServicesInterface;
using DataAccessLayer.Model;
using DataAccessLayer.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly ISubjectRepository _subjectRepository;

        public ScheduleService(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public async Task<IEnumerable<Schedules>> GetAllSchedules()
        {
            var schedules = await _scheduleRepository.GetAll();
            return schedules;
        }

        public async Task<Schedules> GetScheduleById(int id)
        {
            var user = await _scheduleRepository.GetById(id);
            return user;
        }

        public async Task<Schedules> InsertSchedule(Schedules schedule)
        {
            var result = await _scheduleRepository.Insert(schedule);
            return result;
        }

        public async Task<Schedules> UpdateSchedule(Schedules schedule)
        {
            var result = await _scheduleRepository.UpdateSchedule(schedule);
            return result;
        }

        public async Task<Schedules> DeleteSchedule(int id)
        {
            var schedule = await _scheduleRepository.GetById(id);
            await _scheduleRepository.Delete(schedule);
            
            return schedule;
        }

        public async Task<IEnumerable<Schedules>> GetSchedulesByUser(int userId)
        {
            var schedules = await _scheduleRepository.GetSchedulesByUser(userId);
            return schedules;
        }
    }
}
