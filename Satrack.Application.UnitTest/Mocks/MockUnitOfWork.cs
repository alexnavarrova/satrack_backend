
using Moq;
using Microsoft.EntityFrameworkCore;
using Satrack.Infraestructure.Persistence;
using Satrack.Infraestructure.Repositories;


namespace Satrack.Application.UnitTest.Mocks
{
    public class MockUnitOfWork
	{
        public static Mock<UnitOfWork> GetUnitOfWork()
        {
            Guid dbContextId = Guid.NewGuid();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"ApplicationDbContext-{dbContextId}")
                .Options;

            var streamerDbContextFake = new ApplicationDbContext(options);
            streamerDbContextFake.Database.EnsureDeleted();
            var mockUnitOfWork = new Mock<UnitOfWork>(streamerDbContextFake);


            return mockUnitOfWork;
        }
    }
}

