using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using teleperformance_case3.Application.Commands;
using teleperformance_case3.Application.Common;
using teleperformance_case3.Application.Common.Interfaces;
using teleperformance_case3.Domain.Entities;

namespace teleperformance_case3.Application.Handlers.Commands;

internal class CreateShoppingListItemHandler : IRequestHandler<CreateShoppingListItemCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateShoppingListItemHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<bool> Handle(CreateShoppingListItemCommand request, CancellationToken cancellationToken)
    {
        var shoppingList =
            await _context.ShoppingLists
                .FirstOrDefaultAsync(e => e.IsActive && e.Id == request.ShoppingListId, cancellationToken);

        var shoppingListItem = _mapper.Map<ShoppingListItem>(request);
        shoppingListItem.Product =
            await _context.Products
                .FirstOrDefaultAsync(e => e.IsActive && e.Id == request.ProductId, cancellationToken);
        shoppingListItem.ShoppingList = shoppingList;

        try
        {
            await _context.ShoppingListItems.AddAsync(shoppingListItem, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new ApiException(new Error
            {
                Message = "Veritabanına kaydederken bir hata oluştu.",
                Errors = new List<string> {ex.Message}
            });
        }

        return true;
    }
}