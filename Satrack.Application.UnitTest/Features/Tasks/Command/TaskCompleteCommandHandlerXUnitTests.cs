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
    public class TaskCompleteCommandHandlerXUnitTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<ILogger<CompleteTaskCommandHandler>> _logger;


        public TaskCompleteCommandHandlerXUnitTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _logger = new Mock<ILogger<CompleteTaskCommandHandler>>();

            MockTaskRepository.AddDataTaskRepository(_unitOfWork.Object.ApplicationDbContext);

            
        }


        [Fact]
        public async Task CompleteTaskCommandInputCategoriaReturnsUnit()
        {
            var taskComplete = new CompleteTaskCommand
            {
                Id = new Guid("c65da35f-6af0-472b-9a94-7c4f9d3db488"),
                IsCompleted = true,
            };

            var handler = new CompleteTaskCommandHandler(_unitOfWork.Object, _mapper, _logger.Object);

            var result = await handler.Handle(taskComplete, CancellationToken.None);

            result.ShouldBeOfType<Unit>();
        }

        [Fact]
        public async Task CreateCategoryCommand_InputCategoria_ThrowException_NoChangeStatus()
        {
            var taskComplete = new CompleteTaskCommand
            {
                Id = new Guid("00000000-0000-0000-0000-000000000000"),
                IsCompleted = true,
            };

            var handler = new CompleteTaskCommandHandler(_unitOfWork.Object, _mapper, _logger.Object);

            await Assert.ThrowsAsync<Exception>(async () => await handler.Handle(taskComplete, CancellationToken.None));

        }

    }
}

