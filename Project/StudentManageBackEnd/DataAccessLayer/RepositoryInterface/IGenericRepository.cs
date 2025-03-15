using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.RepositoryInterface
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetById(int id);
        public Task<T> Insert(T entity);
        public Task<T> Update(T entity);
        public Task<T> Delete(T entity);
        public Task<int> Save();
    }
}
