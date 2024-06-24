
using Microsoft.EntityFrameworkCore.Storage;
using Satrack.Domain;

namespace Satrack.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        ITaskRepository TaskRepository { get; }

        IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel;
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync(IDbContextTransaction transaction);
        Task RollbackTransactionAsync(IDbContextTransaction transaction);
        Task<int> Complete();
    }
}

