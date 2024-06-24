using MediatR;
using AutoMapper;
using Satrack.Domain.Entities;
using Microsoft.Extensions.Logging;
using Satrack.Application.Contracts.Persistence;

namespace Satrack.Application.Features.Tasks.Commands.CreateTask
{

    public class CompleteTaskCommandHandler : IRequestHandler<CompleteTaskCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CompleteTaskCommandHandler> _logger;


        public CompleteTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CompleteTaskCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }


        public async Task<Unit> Handle(CompleteTaskCommand request, CancellationToken cancellationToken)
        {
            var taskEntity = await _unitOfWork.TaskRepository.GetByIdAsync(request.Id);

            if (taskEntity == null)
            {
                _logger.LogError("Task does not exist");
                throw new Exception("Task does not exist");
            }

            taskEntity.IsCompleted = request.IsCompleted;

            _unitOfWork.TaskRepository.UpdateEntity(taskEntity);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                _logger.LogError("Task could not be updated");
                throw new Exception("Task could not be updated");
            }

            return Unit.Value;

        }

    }
}

