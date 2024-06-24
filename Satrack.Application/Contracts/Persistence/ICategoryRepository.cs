using Satrack.Domain.Entities;

namespace Satrack.Application.Contracts.Persistence
{
    public interface ICategoryRepository : IAsyncRepository<CategoryEntity>
    {
    }
}

