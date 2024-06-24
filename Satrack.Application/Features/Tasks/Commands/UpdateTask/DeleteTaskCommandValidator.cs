using FluentValidation;
using Satrack.Application.Common;
using Satrack.Application.Features.Tasks.Commands.DeleteTask;

namespace Satrack.Application.Features.Tasks.Commands.UpdateTask
{
	public class DeleteTaskCommandValidator : AbstractValidator<DeleteTaskCommand>
    {

        public DeleteTaskCommandValidator()
        {

            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("{Id} can not be empty")
                .Must(x => x.BeAValidGuid()).WithMessage("{Id} must be a valid GUID.");;

        }
    }
}