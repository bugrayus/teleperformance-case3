using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using teleperformance_case3.Application.Common.Interfaces;
using teleperformance_case3.Application.Models;
using teleperformance_case3.Application.Queries;

namespace teleperformance_case3.Application.Handlers.Queries;

public class GetShoppingListsHandler : IRequestHandler<GetShoppingListsQuery, GetShoppingListsResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetShoppingListsHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<GetShoppingListsResponse> Handle(GetShoppingListsQuery request,
        CancellationToken cancellationToken)
    {
        var shoppingLists =
            await _context.ShoppingLists
                .AsNoTracking()
                .Include(e => e.ShoppingListItems)
                .ThenInclude(e => e.Product)
                .Where(e => e.IsActive)
                .ToListAsync(cancellationToken);

        var shoppingListsResponse = new List<GetShoppingListResponse>();

        foreach (var shoppingList in shoppingLists)
        {
            var shoppingListItemResponses = new List<GetShoppingListItemResponse>();
            foreach (var shoppingListItem in shoppingList.ShoppingListItems)
            {
                var shoppingListItemResponse = _mapper.Map<GetShoppingListItemResponse>(shoppingListItem);
                shoppingListItemResponse.Product = _mapper.Map<GetProductResponse>(shoppingListItem.Product);
                shoppingListItemResponses.Add(shoppingListItemResponse);
            }

            var response = _mapper.Map<GetShoppingListResponse>(shoppingList);
            response.ShoppingListItems = shoppingListItemResponses;
            shoppingListsResponse.Add(response);
        }

        return new GetShoppingListsResponse
        {
            ShoppingLists = shoppingListsResponse
        };
    }
}