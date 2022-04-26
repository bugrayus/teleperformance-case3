using MediatR;
using Microsoft.EntityFrameworkCore;
using teleperformance_case3.Application.Commands;
using teleperformance_case3.Application.Common;
using teleperformance_case3.Application.Common.Interfaces;

namespace teleperformance_case3.Application.Handlers.Commands;

internal class DeleteShoppingListItemsHandler : IRequestHandler<DeleteShoppingListItemsCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeleteShoppingListItemsHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteShoppingListItemsCommand request, CancellationToken cancellationToken)
    {
        var shoppingListItems =
            await _context.ShoppingListItems
                .Where(e =>
                    e.IsActive &&
                    e.ShoppingListId == request.Id)
                .ToListAsync(cancellationToken);

        foreach (var shoppingListItem in shoppingListItems)
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