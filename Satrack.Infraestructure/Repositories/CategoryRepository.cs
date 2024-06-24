using Satrack.Domain.Entities;
using Satrack.Application.Contracts.Persistence;
using Satrack.Infraestructure.Persistence;

namespace Satrack.Infraestructure.Repositories
{
    public class CategoryRepository : RepositoryBase<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {

        }

    }
}

