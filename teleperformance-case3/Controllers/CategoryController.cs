using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using teleperformance_case3.Application.Commands;
using teleperformance_case3.Application.Queries;
using teleperformance_case3.Models;

namespace teleperformance_case3.Controllers;

[Authorize]
[Route("api/category")]
[ApiController]
public class CategoryController : BaseController
{
    private readonly IMapper _mapper;

    public CategoryController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryAsync(int id)
    {
        var query = new GetCategoryQuery
        {
            Id = id
        };
        var response = await Mediator.Send(query);
        return Success("Category details fetched successfully.", response);
    }

    [HttpGet]
    public async Task<IActionResult> GetCategoriesAsync()
    {
        var query = new GetCategoriesQuery();
        var response = await Mediator.Send(query);
        return Success("Categories fetched successfully.", response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategoryAsync(CreateCategoryRequest request)
    {
        var command = _mapper.Map<CreateCategoryCommand>(request);
        var response = await Mediator.Send(command);
        return Success("Category created successfully.", response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoryAsync(int id)
    {
        var command = new DeleteCategoryCommand
        {
            Id = id
        };
        var response = await Mediator.Send(command);
        return Success("Category deleted successfully.", response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategoryAsync(UpdateCategoryRequest request, int id)
    {
        var command = _mapper.Map<UpdateCategoryCommand>(request);
        command.Id = id;
        var response = await Mediator.Send(command);
        return Success("Category updated successfully.", response);
    }
}