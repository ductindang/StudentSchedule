using BusinessLogicLayer.ServicesInterface;
using DataAccessLayer.Model;
using DataAccessLayer.RepositoryInterface;

namespace BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Users>> GetAllUser()
        {
            var users = await _userRepository.GetAll();
            return users;
        }

        public async Task<Users> GetUserById(int id)
        {
            var user = await _userRepository.GetById(id);
            return user;
        }

        public async Task<Users> GetUserByEmailPassword(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailPass(email, password);
            return user;
        }

        public async Task<Users> InsertUser(Users user)
        {
            var result = await _userRepository.Insert(user);
            return result;
        }

        public async Task<Users> UpdateUser(Users user)
        {
            var result = await _userRepository.Update(user);
            return result;
        }

        public async Task<Users> DeleteUser(int id)
        {
            var user = await _userRepository.GetById(id);
            await _userRepository.Delete(user);

            return user;
        }

       
    }
}
