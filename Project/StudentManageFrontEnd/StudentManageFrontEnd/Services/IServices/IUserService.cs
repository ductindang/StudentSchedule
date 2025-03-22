using StudentManageFrontEnd.Models;

namespace StudentManageFrontEnd.Services.IServices
{
    public interface IUserService
    {
        //Task<User> GetUserByEmailPassword(string email, string password);
        Task<string> Login(string email, string password);
        Task<User> InsertUser(User user);
        Task<bool> DeleteUser(string token);
    }
}
