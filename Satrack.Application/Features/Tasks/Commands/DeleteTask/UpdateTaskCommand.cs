using MediatR;

namespace Satrack.Application.Features.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommand : IRequest<Unit>
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Guid CategoryId { get; set; }
    }
}

