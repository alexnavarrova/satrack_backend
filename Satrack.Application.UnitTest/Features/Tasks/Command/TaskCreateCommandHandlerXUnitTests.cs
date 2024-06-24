using Moq;
using Xunit;
using MediatR;
using Shouldly;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Satrack.Application.Mappings;
using Satrack.Application.UnitTest.Mocks;
using Satrack.Infraestructure.Repositories;
using Satrack.Application.Features.Tasks.Commands.CreateTask;

namespace Satrack.Application.UnitTest.Features.Tasks.Command
{
    public class TaskCreateCommandHandlerXUnitTests
	{
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<ILogger<CreateTaskCommandHandler>> _logger;


        public TaskCreateCommandHandlerXUnitTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _logger = new Mock<ILogger<CreateTaskCommandHandler>>();;

            MockCategoryRepository.AddDataCategoryRepository(_unitOfWork.Object.ApplicationDbContext);
            MockTaskRepository.AddDataTaskRepository(_unitOfWork.Object.ApplicationDbContext);
        }

        [Fact]
        public async Task CreateTaskCommandReturnsUnit()
        {
            var taskCreate = new CreateTaskCommand
            {
                Title = "Task 3",
                Description = "Description...",
                CategoryId = new Guid("35ec50a3-8aa0-45bb-b03b-bc205c4cb201")
            };

            var handler = new CreateTaskCommandHandler(_unitOfWork.Object, _mapper, _logger.Object);

            var result = await handler.Handle(taskCreate, CancellationToken.None);

            result.ShouldBeOfType<Unit>();
        }


        [Fact]
        public async Task CreateTaskCommand_ThrowException_CategoryDoesNotExist()
        {
            var taskCreate = new CreateTaskCommand
            {
                Title = "Task 3",
                Description = "Description...",
                CategoryId = new Guid("00000000-0000-0000-0000-000000000000")
            };

            var handler = new CreateTaskCommandHandler(_unitOfWork.Object, _mapper, _logger.Object);

            await Assert.ThrowsAsync<Exception>(async () => await handler.Handle(taskCreate, CancellationToken.None));

        }

    }
}

