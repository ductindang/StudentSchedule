using BusinessLogicLayer.ServicesInterface;
using DataAccessLayer.Model;
using DataAccessLayer.RepositoryInterface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
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

        public async Task<string> Login(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailPass(email, password);
            if (user == null) return null; // Đăng nhập thất bại

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(Users user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("FullName", user.FullName),
                new Claim("UserId", user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpireMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Users> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            return user;
        }
    }
}
