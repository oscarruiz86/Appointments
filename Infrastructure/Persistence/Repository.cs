using Application.Interfaces.Persistence;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace Infrastructure.Persistence
{
    public class Repository<T, TKey> : IRepository<T, TKey> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll() => await _dbSet.ToListAsync();

        public async Task<T?> GetById(TKey id)
        => await _dbSet.FindAsync(id);

        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task Update(T entity)
        {
            _dbSet.Update(entity);
            await Task.CompletedTask;
        }

        public async Task Delete(TKey id)
        {
            var entity = await GetById(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }
        public IQueryable<T> Query()
        => _dbSet.AsQueryable();

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<T> GetByIdOrThrow(TKey id)
        {
            var tenant = await GetById(id);
            if (tenant == null)
                throw new KeyNotFoundException($"el {id} no existe");

            return tenant;
        }
    }
}
