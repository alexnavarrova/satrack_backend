using MediatR;

namespace Satrack.Application.Features.Tasks.Commands.CreateTask
{
    public class CompleteTaskCommand : IRequest<Unit>
    {
        public required Guid Id { get; set; }
        public required bool IsCompleted { get; set; }
    }
}

