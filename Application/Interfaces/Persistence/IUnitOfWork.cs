namespace Application.Interfaces.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T, TKey> Repository<T, TKey>() where T : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
