using MediatR;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Satrack.Application.Contracts.Persistence;

namespace Satrack.Application.Features.Tasks.Commands.DeleteTask
{

    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteTaskCommandHandler> _logger;


        public DeleteTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeleteTaskCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }


        public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var taskEntity = await _unitOfWork.TaskRepository.GetByIdAsync(request.Id);

            if (taskEntity == null)
            {
                _logger.LogError("Task does not exist");
                throw new Exception("Task does not exist");
            }

            _unitOfWork.TaskRepository.DeleteEntity(taskEntity);

            try
            {
                var result = await _unitOfWork.Complete();

                if (result <= 0)
                {
                    _logger.LogError("Task could not be updated");
                    throw new Exception("Task could not be updated");
                }
            }
            catch (Exception)
            {
                throw new Exception("Task could not be updated");
            }

            return Unit.Value;

        }

    }
}

