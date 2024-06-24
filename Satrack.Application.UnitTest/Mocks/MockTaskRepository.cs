using AutoFixture;
using Satrack.Domain.Entities;
using Satrack.Infraestructure.Persistence;

namespace Satrack.Application.UnitTest.Mocks
{
    public class MockTaskRepository
	{
        public static void AddDataTaskRepository(ApplicationDbContext tektonContextFake)
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var tasks = fixture.CreateMany<TaskEntity>().ToList();

            tasks.Add(fixture.Build<TaskEntity>()
                .With(tr => tr.Id, new Guid("c65da35f-6af0-472b-9a94-7c4f9d3db488"))
                .With(tr => tr.Title, "Title 1")
                .With(tr => tr.Description, "Description 1")
                .Create()
            );


            tasks.Add(fixture.Build<TaskEntity>()
                .With(tr => tr.Id, new Guid("c20c5f15-1174-4e54-8d53-0646acac8b78"))
                .With(tr => tr.Title, "Title 2")
                .With(tr => tr.Description, "Description 2")
                .With(tr => tr.CategoryId, new Guid("35ec50a3-8aa0-45bb-b03b-bc205c4cb201"))
                .Create()
            );

            tektonContextFake.Tasks !.AddRange(tasks);
            tektonContextFake.SaveChanges();
        }
    }
}

