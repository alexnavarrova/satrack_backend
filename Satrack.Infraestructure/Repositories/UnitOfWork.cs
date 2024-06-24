using Satrack.Domain;
using System.Collections;
using Satrack.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore.Storage;
using Satrack.Infraestructure.Persistence;

namespace Satrack.Infraestructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly ApplicationDbContext _context;
        public ApplicationDbContext ApplicationDbContext => _context;


        private ICategoryRepository _categoryRepository;
        private ITaskRepository _taskRepository;

        public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_context);
        public ITaskRepository TaskRepository => _taskRepository ??= new TaskRepository(_context);

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel
        {
            if (_repositories == null)
            {
                _repositories = [];
            }

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryBase<>);
                var respositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, respositoryInstance);
            }

            return (IAsyncRepository<TEntity>)_repositories[type];
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            try
            {
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                throw new Exception("Error when try CommitTransactionAsync");
            }
        }

        public async Task RollbackTransactionAsync(IDbContextTransaction transaction)
        {
            await transaction.RollbackAsync();
        }

    }
}

