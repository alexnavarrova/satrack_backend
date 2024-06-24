using MediatR;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Satrack.Application.Features.Book.Queries.GetBooksList;
using Satrack.Application.Features.Categories.Commands.CreateCategory;
using Satrack.Application.Features.Categories.Queries.GetCategoriesList;

namespace Satrack.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Create Category.
    /// </summary>
    /// <param name="CreateCategoryCommand">The params to create category</param>
    /// <returns>Unit</returns>
    [HttpPost(Name = "CreateCategory")]
    [Authorize(Roles = "Operator")]
    public async Task<ActionResult<Unit>> CreateCategoryAsync([FromBody] CreateCategoryCommand requestCommand) => await _mediator.Send(requestCommand);

    /// <summary>
    /// Get All Categories.
    /// </summary>
    /// <returns>List of Categories</returns>
    [HttpGet(Name = "GetCategories")]
    [Authorize(Roles = "Operator")]
    [ProducesResponseType(typeof(IEnumerable<CategoryList>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<List<CategoryList>>> GetCategoriesAsync() => await _mediator.Send(new GetCategoriesListQuery());
}
