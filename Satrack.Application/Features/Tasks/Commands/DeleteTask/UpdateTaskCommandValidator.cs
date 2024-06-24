using FluentValidation;
using Satrack.Application.Common;

namespace Satrack.Application.Features.Tasks.Commands.UpdateTask
{
	public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {

        public UpdateTaskCommandValidator()
        {

            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("{Id} can not be empty")
                .Must(x => x.BeAValidGuid()).WithMessage("{Id} must be a valid GUID.");


            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{Title} can not be empty")
                .NotNull()
                .MaximumLength(100).WithMessage("{Title} cannot exceed 100 characters");

            RuleFor(p => p.DueDate)
               .NotEmpty().NotNull().WithMessage("{DueDate} cannot be empty")
               .Must(x => x.BeAValidDate()).WithMessage("{DueDate} is not valid.");

            RuleFor(p => p.Description)
              .NotEmpty().NotNull().WithMessage("{Description} can not be empty");

            RuleFor(p => p.CategoryId)
                .NotEmpty().WithMessage("{CategoryId} can not be blank or zero")
                .Must(x => x.BeAValidGuid()).WithMessage("{CategoryId} must be a valid GUID.");

        }
    }
}