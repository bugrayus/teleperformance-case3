using FluentValidation;
using Microsoft.EntityFrameworkCore;
using teleperformance_case3.Application.Commands;
using teleperformance_case3.Application.Common.Interfaces;

namespace teleperformance_case3.Application.Validators.Commands;

public class DeleteShoppingListItemValidator : AbstractValidator<DeleteShoppingListItemCommand>
{
    public DeleteShoppingListItemValidator(IApplicationDbContext context)
    {
        RuleFor(e => e.ProductId)
            .GreaterThan(0)
            .MustAsync(async (id, _) =>
            {
                var product = await context.Products
                    .FirstOrDefaultAsync(e => e.IsActive && e.Id == id);
                return product != null;
            });
        RuleFor(e => e.ShoppingListId)
            .GreaterThan(0)
            .MustAsync(async (id, _) =>
            {
                var shoppingList = await context.ShoppingLists
                    .FirstOrDefaultAsync(e => e.IsActive && e.Id == id);
                return shoppingList != null;
            });
    }
}