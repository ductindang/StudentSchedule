using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.ServicesInterface
{
    public interface IUserService
    {
        public Task<IEnumerable<Users>> GetAllUser();
        public Task<Users> GetUserById(int id);
        public Task<Users> InsertUser(Users user);
        public Task<Users> UpdateUser(Users user);
        public Task<Users> DeleteUser(int id);
        Task<Users> GetUserByEmailPassword(string email, string password);
        public Task<string> Login(string email, string password);
        public Task<Users> GetUserByEmail(string email);
    }
}
