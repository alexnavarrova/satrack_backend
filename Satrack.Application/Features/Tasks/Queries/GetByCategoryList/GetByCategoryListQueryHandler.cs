using MediatR;
using AutoMapper;
using Satrack.Application.Contracts.Persistence;
using Satrack.Application.Features.Task.Queries.GetTaskList;

namespace Satrack.Application.Features.Task.Queries.GetByCategoryList
{
    public class GetByCategoryListQueryHandler : IRequestHandler<GetByCategoryListQuery, List<TaskDetail>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public GetByCategoryListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TaskDetail>> Handle(GetByCategoryListQuery request, CancellationToken cancellationToken)
        {
            var task = await _unitOfWork.TaskRepository.GetAllTaskByCategoryId(request.CaegoryId);

            return _mapper.Map<List<TaskDetail>>(task);

        }
    }
}

