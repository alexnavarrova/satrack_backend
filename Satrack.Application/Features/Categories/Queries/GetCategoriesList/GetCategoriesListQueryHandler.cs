using MediatR;
using AutoMapper;
using Satrack.Application.Contracts.Persistence;

namespace Satrack.Application.Features.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, List<CategoryList>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoriesListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CategoryList>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();

            return _mapper.Map<List<CategoryList>>(categories);
        }
    }
}

