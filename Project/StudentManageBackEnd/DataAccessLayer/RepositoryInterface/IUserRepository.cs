using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.RepositoryInterface
{
    public interface IUserRepository : IGenericRepository<Users>
    {
        public Task<Users> GetUserByEmailPass(string email, string password);
    }
}
