using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using teleperformance_case3.Application.Commands;
using teleperformance_case3.Application.Queries;
using teleperformance_case3.Models;

namespace teleperformance_case3.Controllers;

[Authorize]
[ApiController]
[Route("api/products")]
public class ProductController : BaseController
{
    private readonly IMapper _mapper;

    public ProductController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductAsync(int id)
    {
        var query = new GetProductQuery
        {
            Id = id
        };
        var response = await Mediator.Send(query);
        return Success("Product details fetched successfully.", response);
    }

    [HttpGet]
    public async Task<IActionResult> GetProductsAsync()
    {
        var query = new GetProductsQuery();
        var response = await Mediator.Send(query);
        return Success("Products fetched successfully.", response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductAsync(CreateProductRequest request)
    {
        var command = _mapper.Map<CreateProductCommand>(request);
        var response = await Mediator.Send(command);
        return Success("Product created successfully.", response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductAsync(int id)
    {
        var command = new DeleteProductCommand
        {
            Id = id
        };
        var response = await Mediator.Send(command);
        return Success("Product deleted successfully.", response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProductAsync(UpdateProductRequest request, int id)
    {
        var command = _mapper.Map<UpdateProductCommand>(request);
        command.Id = id;
        var response = await Mediator.Send(command);
        return Success("Product updated successfully.", response);
    }

    [HttpPost("{productId}/shopping-lists/{shoppingListId}")]
    public async Task<IActionResult> CreateShoppingListItemAsync(CreateShoppingListItemRequest request, int productId,
        int shoppingListId)
    {
        var command = _mapper.Map<CreateShoppingListItemCommand>(request);
        command.ProductId = productId;
        command.ShoppingListId = shoppingListId;
        var response = await Mediator.Send(command);
        return Success("Shopping list item added successfully.", response);
    }
}