using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using teleperformance_case3.Application.Common.Interfaces;
using teleperformance_case3.Application.Models;
using teleperformance_case3.Application.Queries;

namespace teleperformance_case3.Application.Handlers.Queries;

public class GetShoppingListHandler : IRequestHandler<GetShoppingListQuery, GetShoppingListResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetShoppingListHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<GetShoppingListResponse> Handle(GetShoppingListQuery request, CancellationToken cancellationToken)
    {
        var shoppingList =
            await _context.ShoppingLists
                .AsNoTracking()
                .Include(e => e.ShoppingListItems)
                .ThenInclude(e => e.Product)
                .FirstOrDefaultAsync(e => e.IsActive && e.Id == request.Id, cancellationToken);

        var response = _mapper.Map<GetShoppingListResponse>(shoppingList);

        if (shoppingList?.ShoppingListItems == null) return response;
        var shoppingListItemResponses = new List<GetShoppingListItemResponse>();
        foreach (var shoppingListItem in shoppingList.ShoppingListItems)
        {
            var shoppingListItemResponse = _mapper.Map<GetShoppingListItemResponse>(shoppingListItem);
            shoppingListItemResponse.Product = _mapper.Map<GetProductResponse>(shoppingListItem.Product);
            shoppingListItemResponses.Add(shoppingListItemResponse);
        }

        response.ShoppingListItems = shoppingListItemResponses;

        return response;
    }
}