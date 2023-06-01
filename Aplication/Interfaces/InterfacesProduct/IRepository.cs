using System.Linq.Expressions;

namespace Aplication.Interfaces.InterfacesProduct;
public interface IRepository<T> 
{
    Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> expression);
    Task<T?> GetByIdAsync(int id);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int Id);
}

