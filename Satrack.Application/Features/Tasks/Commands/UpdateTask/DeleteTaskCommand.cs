using MediatR;

namespace Satrack.Application.Features.Tasks.Commands.DeleteTask
{
    public class DeleteTaskCommand : IRequest<Unit>
    {
        public required Guid Id { get; set; }
    }
}

