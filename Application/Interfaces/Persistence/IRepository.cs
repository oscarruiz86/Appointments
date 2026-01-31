using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Interfaces.Persistence
{
    public interface IRepository<T, TKey> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(TKey id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(TKey id);
        IQueryable<T> Query();
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdOrThrow(TKey id);
    }
}
