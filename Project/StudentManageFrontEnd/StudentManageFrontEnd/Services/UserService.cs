using StudentManageFrontEnd.Models;
using StudentManageFrontEnd.Services.IServices;

namespace StudentManageFrontEnd.Services
{
    public class UserService : IUserService
    {
        private HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<IEnumerable<User>> GetUserByEmailPassword(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
