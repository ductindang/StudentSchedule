using StudentManageFrontEnd.Models;

namespace StudentManageFrontEnd.Services.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUserByEmailPassword(string email, string password);
    }
}
