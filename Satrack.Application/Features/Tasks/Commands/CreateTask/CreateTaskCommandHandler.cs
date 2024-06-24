using MediatR;
using AutoMapper;
using Satrack.Domain.Entities;
using Microsoft.Extensions.Logging;
using Satrack.Application.Contracts.Persistence;

namespace Satrack.Application.Features.Tasks.Commands.CreateTask
{

    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateTaskCommandHandler> _logger;


        public CreateTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateTaskCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }


        public async Task<Unit> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var categoryEntity = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId);

            if (categoryEntity == null)
            {
                _logger.LogError("Category does not exist");
                throw new Exception("Category does not exist");
            }

            var addTask = _mapper.Map<TaskEntity>(request);

            _unitOfWork.TaskRepository.AddEntity(addTask);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                _logger.LogError("Task could not be created");
                throw new Exception("Task could not be created");
            }

            return Unit.Value;

        }

    }
}

