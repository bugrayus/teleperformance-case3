using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using teleperformance_case3.Application.Commands;
using teleperformance_case3.Application.Queries;
using teleperformance_case3.Models;

namespace teleperformance_case3.Controllers;

[ApiController]
[Authorize]
[Route("api/users/shopping-lists")]
public class ShoppingListController : BaseController
{
    private readonly IMapper _mapper;

    public ShoppingListController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateShoppingListAsync(CreateShoppingListRequest request)
    {
        var command = _mapper.Map<CreateShoppingListCommand>(request);
        var response = await Mediator.Send(command);
        return Success("Shopping list created successfully.", response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShoppingListAsync(int id)
    {
        var command = new DeleteShoppingListCommand
        {
            Id = id
        };
        var response = await Mediator.Send(command);
        return Success("Shopping list deleted successfully.", response);
    }

    [HttpGet]
    public async Task<IActionResult> GetShoppingListsAsync()
    {
        var query = new GetShoppingListsQuery();
        var response = await Mediator.Send(query);
        return Success("Shopping lists fetched successfully.", response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetShoppingListAsync(int id)
    {
        var query = new GetShoppingListQuery
        {
            Id = id
        };
        var response = await Mediator.Send(query);
        return Success("Shopping list fetched successfully.", response);
    }
}