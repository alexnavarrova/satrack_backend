using MediatR;
using Satrack.Application.Features.Task.Queries.GetTaskList;

namespace Satrack.Application.Features.Task.Queries.GetTaskList
{
	public class GetTaskListQuery : IRequest<List<TaskDetail>>
    {
    }
}

