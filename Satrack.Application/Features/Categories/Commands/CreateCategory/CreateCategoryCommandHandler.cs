using MediatR;
using AutoMapper;
using Satrack.Domain.Entities;
using Satrack.Application.Contracts.Persistence;
using Satrack.Application.Features.Categories.Commands.CreateCategory;
using Microsoft.Extensions.Logging;

namespace Satrack.Application.Features.Category.Commands.CreateBook
{

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateCategoryCommandHandler> _logger;


        public CreateCategoryCommandHandler(ILogger<CreateCategoryCommandHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
		{
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Unit> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {

            var addCategory = _mapper.Map<CreateCategoryCommand, CategoryEntity>(request);

            _unitOfWork.CategoryRepository.AddEntity(addCategory);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                _logger.LogError("No se pudo insertar el libro");
                throw new Exception("No se pudo insertar el libro");
            }   

            return Unit.Value;

        }

    }
}

