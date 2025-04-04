using StudentManageFrontEnd.Models;

namespace StudentManageFrontEnd.Services.IServices
{
    public interface ISubjectService
    {
        Task<IEnumerable<Subject>> GetAllSubject(string token);
        Task<Subject> GetSubjectById(int id, string token);
        Task<Subject> InsertSubject(Subject subject, string token);
        Task<Subject> UpdateSubject(Subject subject, string token);
        Task<Subject> DeleteSubject(int id, string token);
    }
}
