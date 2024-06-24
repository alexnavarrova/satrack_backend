using MediatR;
using Satrack.Application.Features.Task.Queries.GetTaskList;

namespace Satrack.Application.Features.Task.Queries.GetByCategoryList
{
	public class GetByCategoryListQuery : IRequest<List<TaskDetail>>
    {
        public Guid CaegoryId { get; set; }
    }
}

