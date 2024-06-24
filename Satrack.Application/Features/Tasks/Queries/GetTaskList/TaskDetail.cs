
namespace Satrack.Application.Features.Task.Queries.GetTaskList
{
	public class TaskDetail
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required Guid CategoryId { get; set; }
        public required string CategoryName { get; set; }
        public bool IsCompleted { get; set; }
        public required string DueDate{ get; set; }
        public DateTime CreatedDate { get; set; }
        

    }
}

