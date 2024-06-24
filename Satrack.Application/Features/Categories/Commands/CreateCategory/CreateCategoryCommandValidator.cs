using FluentValidation;
using Satrack.Application.Features.Categories.Commands.CreateCategory;

namespace Satrack.Application.Features.Category.Commands.CreateCategory
{
	public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {

        public CreateCategoryCommandValidator()
        {
            RuleFor(p => p.Name)
               .NotEmpty().WithMessage("{Name} cannot be empty")
               .NotNull().WithMessage("{Name} cannot be null");
        }

    }
}