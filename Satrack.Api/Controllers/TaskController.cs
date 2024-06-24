using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Satrack.Application.Features.Tasks.Commands.CreateTask;
using Satrack.Application.Features.Task.Queries.GetTaskList;
using Satrack.Application.Features.Tasks.Commands.UpdateTask;
using Satrack.Application.Features.Task.Queries.GetByCategoryList;
using Satrack.Application.Features.Tasks.Commands.DeleteTask;

namespace Satrack.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TaskController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Create Task.
    /// </summary>
    /// <param name="CreateTaskCommand">The params to create task.</param>
    /// <returns>Unit</returns>
    [HttpPost(Name = "CreateTask")]
    [Authorize(Roles = "Operator")]
    public async Task<ActionResult<Unit>> CreateTaskAsync([FromBody] CreateTaskCommand requestCommand) => await _mediator.Send(requestCommand);


    /// <summary>
    /// Update Task.
    /// </summary>
    /// <param name="UpdateTaskCommand">The params to update task.</param>
    /// <returns>Unit</returns>
    [HttpPut(Name = "UpdateTask")]
    [Authorize(Roles = "Operator")]
    public async Task<ActionResult<Unit>> UpdateTaskAsync([FromBody] UpdateTaskCommand requestCommand) => await _mediator.Send(requestCommand);


    /// <summary>
    /// Get All Task
    /// </summary>
    /// <returns>List of task</returns>
    // GET: api/Task
    [HttpGet(Name = "GetTasks")]
    [Authorize(Roles = "Operator")]
    public async Task<ActionResult<List<TaskDetail>>> GetTasksAsync() => await _mediator.Send(new GetTaskListQuery());


    /// <summary>
    /// Complee Task.
    /// </summary>
    /// <param name="CompleteTaskCommand">The params to complete task.</param>
    /// <returns>Unit</returns>
    [HttpPut("complete", Name = "CompleteTask")]
    [Authorize(Roles = "Operator")]
    public async Task<ActionResult<Unit>> CompleteTask([FromBody] CompleteTaskCommand requestCommand) => await _mediator.Send(requestCommand);


    /// <summary>
    /// Get Task By Category
    /// </summary>
    /// <returns>List of task by category</returns>
    // GET: api/Task/Category
    [HttpGet("category/{categoryId}", Name = "GetTaskByCategory")]
    [Authorize(Roles = "Operator")]
    public async Task<ActionResult<List<TaskDetail>>> GetTaskByCategory(Guid categoryId)
            => await _mediator.Send(new GetByCategoryListQuery() { CaegoryId = categoryId });


    /// <summary>
    /// Delete Task
    /// </summary>
    /// <returns>void</returns>
    // Delete: api/Task
    [HttpDelete("{id}", Name = "DeleteById")]
    [Authorize(Roles = "Operator")]
    public async Task<ActionResult<Unit>> DeleteById(Guid id)
            => await _mediator.Send(new DeleteTaskCommand() { Id = id });


}
