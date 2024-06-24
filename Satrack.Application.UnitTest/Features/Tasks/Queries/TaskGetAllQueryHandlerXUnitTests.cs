using Moq;
using Xunit;
using AutoMapper;
using Satrack.Application.Mappings;
using Satrack.Application.UnitTest.Mocks;
using Satrack.Infraestructure.Repositories;
using Satrack.Application.Features.Task.Queries.GetTaskList;

namespace Satrack.Application.UnitTest.Features.Tasks.Queries
{
    public class TaskGetAllQueryHandlerXUnitTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;

        public TaskGetAllQueryHandlerXUnitTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            MockTaskRepository.AddDataTaskRepository(_unitOfWork.Object.ApplicationDbContext);

        }

        [Fact]
        public async Task GetTaskAllTest()
        {

            var handler = new GetTaskListQueryHandler(_unitOfWork.Object, _mapper);

            var request = new GetTaskListQuery();

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.IsAssignableFrom<List<TaskDetail>>(result);

        }

        [Fact]
        public async Task GetTaskByIdTest()
        {

            var handler = new GetTaskListQueryHandler(_unitOfWork.Object, _mapper);

            var request = new GetTaskListQuery();

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.IsAssignableFrom<List<TaskDetail>>(result);

        }

    }

   
}

