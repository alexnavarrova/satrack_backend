using MediatR;

namespace Satrack.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<Unit>
    {

        public required string Name { get; set; }
    }
}

