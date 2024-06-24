using MediatR;
using AutoMapper;
using Satrack.Application.Contracts.Persistence;

namespace Satrack.Application.Features.Task.Queries.GetTaskList
{
    public class GetTaskListQueryHandler : IRequestHandler<GetTaskListQuery, List<TaskDetail>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public GetTaskListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TaskDetail>> Handle(GetTaskListQuery request, CancellationToken cancellationToken)
        {
            var task = await _unitOfWork.TaskRepository.GetAllTask();

            return _mapper.Map<List<TaskDetail>>(task);

        }
    }
}

