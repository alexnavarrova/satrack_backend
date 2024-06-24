using MediatR;
using Satrack.Application.Features.Categories.Queries.GetCategoriesList;

namespace Satrack.Application.Features.Book.Queries.GetBooksList
{
    public class GetCategoriesListQuery : IRequest<List<CategoryList>>
    {
	}
}

