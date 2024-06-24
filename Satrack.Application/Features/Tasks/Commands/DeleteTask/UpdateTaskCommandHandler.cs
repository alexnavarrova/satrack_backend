using MediatR;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Satrack.Application.Contracts.Persistence;

namespace Satrack.Application.Features.Tasks.Commands.UpdateTask
{

    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateTaskCommandHandler> _logger;


        public UpdateTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateTaskCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }


        public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var taskEntity = await _unitOfWork.TaskRepository.GetByIdAsync(request.Id);
            var categoryEntity = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId);

            if (taskEntity == null || categoryEntity == null)
            {
                _logger.LogError("Task or Category does not exist");
                throw new Exception("Task or Category does not exist");
            }

            taskEntity.Title = request.Title;
            taskEntity.Description = request.Description;
            taskEntity.DueDate = request.DueDate;
            taskEntity.CategoryId = request.CategoryId;

            _unitOfWork.TaskRepository.UpdateEntity(taskEntity);

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

