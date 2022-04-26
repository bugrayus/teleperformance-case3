using MediatR;
using Microsoft.EntityFrameworkCore;
using teleperformance_case3.Application.Commands;
using teleperformance_case3.Application.Common;
using teleperformance_case3.Application.Common.Interfaces;

namespace teleperformance_case3.Application.Handlers.Commands;

internal class DeleteShoppingListHandler : IRequestHandler<DeleteShoppingListCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeleteShoppingListHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteShoppingListCommand request, CancellationToken cancellationToken)
    {
        var shoppingList =
            await _context.ShoppingLists.FirstOrDefaultAsync(e => e.IsActive && e.Id == request.Id, cancellationToken);

        try
        {
            _context.ShoppingLists.Remove(shoppingList);
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