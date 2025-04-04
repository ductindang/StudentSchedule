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
    public class SubjectRepository : GenericRepository<Subjects>, ISubjectRepository
    {
        private readonly ApplicationDbContext _context;

        public SubjectRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subjects>> GetAllSubjectsFollowUser(int userId)
        {
            var subjectFollowUser = await _context.Subjects.Where(s => s.UserId == userId).ToListAsync();
            return subjectFollowUser;
        }

        public async Task<bool> CheckSubjectExistInSchedule(int subjectId)
        {
            var subject = await _context.Schedules.AnyAsync(sc => sc.SubjectId == subjectId);
            return subject;
        }
    }
}
