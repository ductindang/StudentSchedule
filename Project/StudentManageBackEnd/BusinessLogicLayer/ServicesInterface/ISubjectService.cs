using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.ServicesInterface
{
    public interface ISubjectService
    {
        public Task<IEnumerable<Subjects>> GetAllSubjects(int userId);
        public Task<Subjects> GetSubjectById(int id);
        public Task<Subjects> InsertSubject(Subjects subject);
        public Task<Subjects> UpdateSubject(Subjects subject);
        public Task<Subjects> DeleteSubject(int id);
        public Task<bool> CheckSubjectExistInSchedule(int subjectId);
        public Task<IEnumerable<Subjects>> DeleteSubjectsInUser(int userId);
    }
}
