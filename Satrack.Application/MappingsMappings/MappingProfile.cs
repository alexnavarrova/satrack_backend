using AutoMapper;
using Satrack.Domain.Entities;
using Satrack.Application.Features.Task.Queries.GetTaskList;
using Satrack.Application.Features.Tasks.Commands.CreateTask;
using Satrack.Application.Features.Categories.Commands.CreateCategory;
using Satrack.Application.Features.Categories.Queries.GetCategoriesList;

namespace Satrack.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCategoryCommand, CategoryEntity>();
            CreateMap<CompleteTaskCommand, TaskEntity>();
            CreateMap<CategoryEntity, CategoryList>();
            CreateMap<TaskEntity, TaskDetail>();
            CreateMap<CreateTaskCommand, TaskEntity>();




        }
    }
}

