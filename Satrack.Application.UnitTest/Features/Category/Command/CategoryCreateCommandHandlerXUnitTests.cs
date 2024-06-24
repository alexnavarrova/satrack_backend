using Moq;
using Xunit;
using MediatR;
using Shouldly;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Satrack.Application.Mappings;
using Satrack.Application.UnitTest.Mocks;
using Satrack.Infraestructure.Repositories;
using Satrack.Application.Features.Categories.Commands.CreateCategory;
using Satrack.Application.Features.Category.Commands.CreateCategory;

namespace Satrack.Application.UnitTest.Features.Category.Command
{
    public class CategoryCreateCommandHandlerXUnitTests
	{
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<ILogger<CreateCategoryCommandHandler>> _logger;


        public CategoryCreateCommandHandlerXUnitTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _logger = new Mock<ILogger<CreateCategoryCommandHandler>>();

            MockCategoryRepository.AddDataCategoryRepository(_unitOfWork.Object.ApplicationDbContext);

            
        }


        [Fact]
        public async Task CreateCategoryCommand_InputCategoria_ReturnsUnit()
        {
            var categoryCreate = new CreateCategoryCommand
            {
                Name = "Categoria 3",
            };

            var handler = new CreateCategoryCommandHandler(_logger.Object, _unitOfWork.Object, _mapper);

            var result = await handler.Handle(categoryCreate, CancellationToken.None);

            result.ShouldBeOfType<Unit>();
        }
        
    }
}

