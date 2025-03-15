using BusinessLogicLayer.ServicesInterface;
using DataAccessLayer.Model;
using DataAccessLayer.RepositoryInterface;

namespace BusinessLogicLayer.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<IEnumerable<Subjects>> GetAllSubjects()
        {
            var subjects = await _subjectRepository.GetAll();
            return subjects;
        }

        public async Task<Subjects> GetSubjectById(int id)
        {
            var subject = await _subjectRepository.GetById(id);
            return subject;
        }

        public async Task<Subjects> InsertSubject(Subjects subject)
        {
            var result = await _subjectRepository.Insert(subject);
            return result;
        }

        public async Task<Subjects> UpdateSubject(Subjects subject)
        {
            var result = await _subjectRepository.Update(subject);
            return result;
        }

        public async Task<Subjects> DeleteSubject(int id)
        {
            var subject = await _subjectRepository.GetById(id);
            await _subjectRepository.Delete(subject);
            return subject;
        }
    }
}
