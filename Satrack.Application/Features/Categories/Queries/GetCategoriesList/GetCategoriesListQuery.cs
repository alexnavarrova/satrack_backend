using MediatR;

namespace Satrack.Application.Features.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQuery : IRequest<List<CategoryList>>
    {
	}
}

