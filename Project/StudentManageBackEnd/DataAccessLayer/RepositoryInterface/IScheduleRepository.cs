﻿using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.RepositoryInterface
{
    public interface IScheduleRepository : IGenericRepository<Schedules>
    {
        public Task<IEnumerable<Schedules>> GetSchedulesByUser(int userId);
        public Task<Schedules> UpdateSchedule(Schedules schedule);
        public Task<IEnumerable<Schedules>> DeleteSchedulesInUser(int userId);
        
    }
}
