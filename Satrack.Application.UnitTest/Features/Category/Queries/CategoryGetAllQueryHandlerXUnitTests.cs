using Moq;
using Xunit;
using AutoMapper;
using Satrack.Application.Mappings;
using Satrack.Application.UnitTest.Mocks;
using Satrack.Infraestructure.Repositories;
using Satrack.Application.Features.Book.Queries.GetBooksList;
using Satrack.Application.Features.Categories.Queries.GetCategoriesList;

namespace Satrack.Application.UnitTest.Features.Book.Queries
{
    public class CategoryGetAllQueryHandlerXUnitTests
	{
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;

        public CategoryGetAllQueryHandlerXUnitTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            MockCategoryRepository.AddDataCategoryRepository(_unitOfWork.Object.ApplicationDbContext);

        }

        [Fact]
        public async Task GetCategoryAllTest()
        {

            var handler = new GetCategoriesListQueryHandler(_unitOfWork.Object, _mapper);

            var request = new GetCategoriesListQuery();

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.IsAssignableFrom<List<CategoryList>>(result);

        }

    }

   
}

