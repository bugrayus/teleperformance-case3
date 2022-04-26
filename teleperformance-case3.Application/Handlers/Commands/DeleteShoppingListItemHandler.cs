using MediatR;
using Microsoft.EntityFrameworkCore;
using teleperformance_case3.Application.Commands;
using teleperformance_case3.Application.Common;
using teleperformance_case3.Application.Common.Interfaces;

namespace teleperformance_case3.Application.Handlers.Commands;

internal class DeleteShoppingListItemHandler : IRequestHandler<DeleteShoppingListItemCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeleteShoppingListItemHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteShoppingListItemCommand request, CancellationToken cancellationToken)
    {
        var shoppingListItem =
            await _context.ShoppingListItems
                .FirstOrDefaultAsync(e =>
                        e.IsActive &&
                        e.ShoppingListId == request.ShoppingListId &&
                        e.ProductId == request.ProductId
                    , cancellationToken);

        try
        {
            _context.ShoppingListItems.Remove(shoppingListItem);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new ApiException(new Error
            {
                Message = "Veritabanından silerken bir hata oluştu.",
                Errors = new List<string> {ex.Message}
            });
        }

        return true;
    }
}