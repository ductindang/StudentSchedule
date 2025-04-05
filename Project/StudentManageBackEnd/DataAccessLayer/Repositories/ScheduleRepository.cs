using DataAccessLayer.Data;
using DataAccessLayer.Model;
using DataAccessLayer.RepositoryInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ScheduleRepository : GenericRepository<Schedules>, IScheduleRepository
    {
        private readonly ApplicationDbContext _context;

        public ScheduleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Schedules>> GetSchedulesByUser(int userId)
        {
            var schedules = await _context.Schedules.Where(s => s.UserId == userId).ToListAsync();
            return schedules;
        }

        public async Task<Schedules> Insert(Schedules schedule)
        {
            bool isConflict = await _context.Schedules.AnyAsync(s =>
                s.UserId == schedule.UserId &&
                s.DayOfWeek == schedule.DayOfWeek && 
                (
                    (schedule.StartTime >= s.StartTime && schedule.StartTime < s.EndTime) ||
                    (schedule.EndTime > s.StartTime && schedule.EndTime <= s.EndTime) ||
                    (schedule.StartTime <= s.StartTime && schedule.EndTime >= s.EndTime)
                )
            );

            if (isConflict)
            {
                throw new InvalidOperationException("Lịch học bị trùng thời gian với một lịch đã có.");
            }

            await _context.Schedules.AddAsync(schedule);
            await _context.SaveChangesAsync();
            return schedule;
        }

        public async Task<Schedules> UpdateSchedule(Schedules schedule)
        {
            // Fetch the current schedule being updated
            var existingSchedule = await _context.Schedules
                .FirstOrDefaultAsync(s => s.Id == schedule.Id);

            if (existingSchedule == null)
            {
                throw new InvalidOperationException("Lịch học không tồn tại.");
            }

            // Check for conflicts with other schedules for the same user on the same day
            var conflictingSchedules = await _context.Schedules
                .Where(s => s.UserId == schedule.UserId && s.DayOfWeek == schedule.DayOfWeek && s.Id != schedule.Id)
                .ToListAsync();

            foreach (var conflict in conflictingSchedules)
            {
                // Check if the start and end times overlap with any of the conflicting schedules
                if ((schedule.StartTime >= conflict.StartTime && schedule.StartTime < conflict.EndTime) ||
                    (schedule.EndTime > conflict.StartTime && schedule.EndTime <= conflict.EndTime) ||
                    (schedule.StartTime <= conflict.StartTime && schedule.EndTime >= conflict.EndTime))
                {
                    throw new InvalidOperationException("Lịch học bị trùng thời gian với một lịch đã có.");
                }
            }

            existingSchedule.SubjectId = schedule.SubjectId;
            existingSchedule.DayOfWeek = schedule.DayOfWeek;
            existingSchedule.StartTime = schedule.StartTime;
            existingSchedule.EndTime = schedule.EndTime;

            // Save the changes to the database
            _context.Schedules.Update(existingSchedule);
            await _context.SaveChangesAsync();

            return existingSchedule;
        }

        public async Task<IEnumerable<Schedules>> DeleteSchedulesInUser(int userId)
        {
            var schedulesToDelete = await GetSchedulesByUser(userId);
            if(schedulesToDelete == null || schedulesToDelete.Count() == 0)
            {
                return Enumerable.Empty<Schedules>();
            }

            _context.Schedules.RemoveRange(schedulesToDelete);
            await _context.SaveChangesAsync();
            return schedulesToDelete;
        }

    }


}
