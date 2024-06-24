using AutoFixture;
using Satrack.Domain.Entities;
using Satrack.Infraestructure.Persistence;

namespace Satrack.Application.UnitTest.Mocks
{
    public class MockCategoryRepository
	{
        public static void AddDataCategoryRepository(ApplicationDbContext tektonContextFake)
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var books = fixture.CreateMany<CategoryEntity>().ToList();

            books.Add(fixture.Build<CategoryEntity>()
                .With(tr => tr.Id, new Guid("35ec50a3-8aa0-45bb-b03b-bc205c4cb201"))
                .With(tr => tr.Name, "Name 1")
                .Create()
            );

            books.Add(fixture.Build<CategoryEntity>()
                .With(tr => tr.Id, new Guid("3b24f377-f2ae-4808-a49f-a13748dff8e2"))
                .With(tr => tr.Name, "Name 2")
                .Create()
            );

            tektonContextFake.Categories !.AddRange(books);
            tektonContextFake.SaveChanges();
        }
    }
}

