using Satrack.Application.Features.Task.Queries.GetTaskList;
using Satrack.Domain.Entities;

namespace Satrack.Application.Contracts.Persistence
{
    public interface ITaskRepository : IAsyncRepository<TaskEntity>
    {
        Task<IEnumerable<TaskDetail>> GetAllTask();
        Task<IEnumerable<TaskDetail>> GetAllTaskByCategoryId(Guid categoryId);

    }
}

