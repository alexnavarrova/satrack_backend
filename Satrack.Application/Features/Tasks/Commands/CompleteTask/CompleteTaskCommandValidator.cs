using FluentValidation;
using Satrack.Application.Common;
using Satrack.Application.Features.Tasks.Commands.CreateTask;

namespace Satrack.Application.Features.Tasks.Commands.CompleteTask
{
	public class CompleteTaskCommandValidator : AbstractValidator<CompleteTaskCommand>
    {

        public CompleteTaskCommandValidator()
        {

            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("{Id} can not be empty")
                .Must(x => x.BeAValidGuid()).WithMessage("{Id} must be a valid GUID.");

        }
    }
}