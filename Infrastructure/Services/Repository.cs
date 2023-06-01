using Aplication.Abstraction;
using Aplication.Interfaces.InterfacesProduct;
using System.Linq.Expressions;

namespace Infrastructure.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly IAplicationDbContext _aplicationDb;

        public Repository(IAplicationDbContext aplicationDb)
        {
            _aplicationDb = aplicationDb;
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            _aplicationDb.Set<T>().Add(entity);
            await _aplicationDb.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<bool> DeleteAsync(int Id)
        {
            var entity = _aplicationDb.Set<T>().Find(Id);
            if (entity != null)
            {
                _aplicationDb.Set<T>().Remove(entity);
                await _aplicationDb.SaveChangesAsync();
                return true;
            }
            return false;

        }

       

        public virtual Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> expression)
        {
            return Task.FromResult(_aplicationDb.Set<T>().Where(expression));
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _aplicationDb.Set<T>().FindAsync(id);
        }

        
        public virtual async Task<T?> UpdateAsync(T entity)
        {
            if (entity != null)
            {
                _aplicationDb.Set<T>().Update(entity);
                await _aplicationDb.SaveChangesAsync();
                return entity;
            }
            return null;
        }
    }
}
