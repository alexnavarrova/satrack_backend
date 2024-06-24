using Moq;
using Xunit;
using MediatR;
using Shouldly;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Satrack.Infraestructure.Repositories;
using Satrack.Application.Features.Tasks.Commands.CreateTask;
using Satrack.Application.Features.Category.Commands.CreateBook;
using Satrack.Application.Features.Categories.Commands.CreateCategory;
using Satrack.Application.Mappings;
using Satrack.Application.UnitTest.Mocks;
using FluentValidation;

namespace Satrack.Application.UnitTest.Features.Book.Command
{
    public class CategoryCreateCommandHandlerXUnitTests
	{
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<ILogger<CreateCategoryCommandHandler>> _logger;
        private readonly Mock<IValidator<CreateTaskCommand>> _mockValidator;
        private readonly CreateTaskCommandHandler _handler;


        public CategoryCreateCommandHandlerXUnitTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _logger = new Mock<ILogger<CreateCategoryCommandHandler>>();
            _mockValidator = new Mock<IValidator<CreateTaskCommand>>();

            MockCategoryRepository.AddDataCategoryRepository(_unitOfWork.Object.ApplicationDbContext);

            
        }


        [Fact]
        public async Task CreateCategoryCommand_InputCategoria_ReturnsUnit()
        {
            var bookCreate = new CreateCategoryCommand
            {
                Name = "Categoria 3",
            };

            var handler = new CreateCategoryCommandHandler(_logger.Object, _unitOfWork.Object, _mapper);

            var result = await handler.Handle(bookCreate, CancellationToken.None);

            result.ShouldBeOfType<Unit>();
        }
        
    }
}

