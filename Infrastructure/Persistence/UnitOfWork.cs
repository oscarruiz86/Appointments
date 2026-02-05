using Application.Interfaces.Persistence;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Concurrent;

namespace Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;
        private readonly ConcurrentDictionary<Type, object> _repositories = new();

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<T, TKey> Repository<T, TKey>()
            where T : class
        {
            return (IRepository<T, TKey>)_repositories.GetOrAdd(
                typeof(T),
                _ => new Repository<T, TKey>(_context)
            );
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);

        public async Task BeginTransactionAsync()
            => _transaction = await _context.Database.BeginTransactionAsync();

        public async Task CommitAsync()
            => await _transaction!.CommitAsync();

        public async Task RollbackAsync()
        {
            if (_transaction != null)
                await _transaction.RollbackAsync();
        }

        public void Dispose()
            => _context.Dispose();
    }

}
