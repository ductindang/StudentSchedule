using StudentManageFrontEnd.Models;

namespace StudentManageFrontEnd.Services.IServices
{
    public interface ISubjectService
    {
        Task<Subject> InsertSubject(Subject subject);
        Task<Subject> UpdateSubject(Subject subject);
        Task<Subject> DeleteSubject(Subject subject);
    }
}
