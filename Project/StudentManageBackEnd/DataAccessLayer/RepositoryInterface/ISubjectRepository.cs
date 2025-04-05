using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.RepositoryInterface
{
    public interface ISubjectRepository : IGenericRepository<Subjects>
    {
        public Task<IEnumerable<Subjects>> GetAllSubjectsFollowUser(int userId);
        public Task<bool> CheckSubjectExistInSchedule(int subjectId);
        public Task<IEnumerable<Subjects>> DeleteSubjectsInUser(int userId);
    }
}
