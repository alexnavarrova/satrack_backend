using Moq;
using Xunit;
using MediatR;
using Shouldly;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Satrack.Application.Mappings;
using Satrack.Application.UnitTest.Mocks;
using Satrack.Infraestructure.Repositories;
using Satrack.Application.Features.Tasks.Commands.UpdateTask;

namespace Satrack.Application.UnitTest.Features.Tasks.Command
{
    public class TaskUpdateCommandHandlerXUnitTests
	{
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<ILogger<UpdateTaskCommandHandler>> _logger;

        public TaskUpdateCommandHandlerXUnitTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _logger = new Mock<ILogger<UpdateTaskCommandHandler>>();;

            MockCategoryRepository.AddDataCategoryRepository(_unitOfWork.Object.ApplicationDbContext);
            MockTaskRepository.AddDataTaskRepository(_unitOfWork.Object.ApplicationDbContext);
        }

        [Fact]
        public async Task CreateTaskCommandReturnsUnit()
        {
            var taskCreate = new UpdateTaskCommand
            {
                Id = new Guid("c65da35f-6af0-472b-9a94-7c4f9d3db488"),
                Title = "Task 3",
                Description = "Description...",
                DueDate = DateTime.Now,
                CategoryId = new Guid("35ec50a3-8aa0-45bb-b03b-bc205c4cb201")
            };

            var handler = new UpdateTaskCommandHandler(_unitOfWork.Object, _mapper, _logger.Object);

            var result = await handler.Handle(taskCreate, CancellationToken.None);

            result.ShouldBeOfType<Unit>();
        }


        [Fact]
        public async Task UpdateTaskCommand_ThrowException_IdDoesNotExist()
        {
            var taskUpdate = new UpdateTaskCommand
            {
                Id = new Guid("00000000-0000-0000-0000-000000000000"),
                Title = "Task 4",
                Description = "Description...",
                DueDate = DateTime.Now,
                CategoryId = new Guid("35ec50a3-8aa0-45bb-b03b-bc205c4cb201")
            };

            var handler = new UpdateTaskCommandHandler(_unitOfWork.Object, _mapper, _logger.Object);

            await Assert.ThrowsAsync<Exception>(async () => await handler.Handle(taskUpdate, CancellationToken.None));

        }

        [Fact]
        public async Task UpdateTaskCommand_ThrowException_CategoryIdDoesNotExist()
        {
            var taskUpdate = new UpdateTaskCommand
            {
                Id = new Guid("c65da35f-6af0-472b-9a94-7c4f9d3db488"),
                Title = "Task 4",
                Description = "Description...",
                DueDate = DateTime.Now,
                CategoryId = new Guid("00000000-0000-0000-0000-000000000000")
            };

            var handler = new UpdateTaskCommandHandler(_unitOfWork.Object, _mapper, _logger.Object);

            await Assert.ThrowsAsync<Exception>(async () => await handler.Handle(taskUpdate, CancellationToken.None));

        }

    }
}

