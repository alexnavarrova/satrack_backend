using MediatR;

namespace Satrack.Application.Features.Tasks.Commands.CreateTask
{
    public class CreateTaskCommand : IRequest<Unit>
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Guid CategoryId { get; set; }
    }
}

