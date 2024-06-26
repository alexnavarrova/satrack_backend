﻿using Moq;
using Microsoft.EntityFrameworkCore;
using Satrack.Infraestructure.Services;

namespace Satrack.UnitTest.Mocks
{
    public class MockUnitOfWork
    {
        public static Mock<UnitOfWork> GetUnitOfWork()
        {
            Guid dbContextId = Guid.NewGuid();
            var options = new DbContextOptionsBuilder<TektonContext>()
                .UseInMemoryDatabase(databaseName: $"TektonContext-{dbContextId}")
                .Options;

            var tektonContextFake = new TektonContext(options);
            tektonContextFake.Database.EnsureDeleted();
            var mockUnitOfWork = new Mock<UnitOfWork>(tektonContextFake);

            return mockUnitOfWork;
        }
    }
}

