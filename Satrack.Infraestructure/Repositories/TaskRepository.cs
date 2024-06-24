using Satrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Satrack.Infraestructure.Persistence;
using Satrack.Application.Contracts.Persistence;
using Satrack.Application.Features.Task.Queries.GetTaskList;

namespace Satrack.Infraestructure.Repositories
{
    public class TaskRepository : RepositoryBase<TaskEntity>, ITaskRepository
    {
        public TaskRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TaskDetail>> GetAllTask()
        {
            var query = from t in _context.Tasks
                        join c in _context.Categories on t.CategoryId equals c.Id
                        orderby t.Id
                        select new TaskDetail
                        {
                            Id = t.Id,
                            Title = t.Title,
                            Description = t.Description,
                            DueDate = t.DueDate.ToString("yyyy-MM-dd"),
                            CreatedDate = t.CreatedDate,
                            IsCompleted = t.IsCompleted,
                            CategoryId = c.Id,
                            CategoryName = c.Name
                        };

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TaskDetail>> GetAllTaskByCategoryId(Guid categoryId)
        {
            var query = from t in _context.Tasks
                        join c in _context.Categories on t.CategoryId equals c.Id
                        where t.CategoryId == categoryId
                        orderby t.Id
                        select new TaskDetail
                        {
                            Id = t.Id,
                            Title = t.Title,
                            Description = t.Description,
                            DueDate = t.DueDate.ToString("yyyy-MM-dd"),
                            CreatedDate = t.CreatedDate,
                            IsCompleted = t.IsCompleted,
                            CategoryId = c.Id,
                            CategoryName = c.Name
                        };

            return await query.ToListAsync();
        }
    }
}

