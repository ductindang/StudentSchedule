using DataAccessLayer.Data;
using DataAccessLayer.RepositoryInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            IEnumerable<T> objects = await _dbSet.ToListAsync();
            return objects;
        }

        public async Task<T> GetById(int id)
        {
            var obj = await _dbSet.FindAsync(id);
            return obj;
        }

        public async Task<T> Insert(T entity)
        {
            if(entity == null)
            {
                return null;
            }

            await _dbSet.AddAsync(entity);
            await Save();
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            if (entity == null)
            {
                return null;
            }
            
            var existingEntity = await _dbSet.FindAsync(GetKey(entity));
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).State = EntityState.Detached;
            }

            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await Save();
            return entity;
        }

        private object GetKey(T entity)
        {
            var keyName = _context.Model.FindEntityType(typeof(T))
                .FindPrimaryKey().Properties.Select(x => x.Name).Single();
            return entity.GetType().GetProperty(keyName).GetValue(entity, null);
        }

        public async Task<T> Delete(T entity)
        {
            if (entity == null)
            {
                return null;
            }
            var key = GetKey(entity);
            var existingEntity = await _dbSet.FindAsync(key);

            if (existingEntity != null)
            {
                _dbSet.Remove(existingEntity);
            }
            else
            {
                _dbSet.Attach(entity);
                _dbSet.Remove(entity);
            }

            await Save();
            return entity;
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

         
    }
}
