using FluentValidation;
using Microsoft.EntityFrameworkCore;
using teleperformance_case3.Application.Commands;
using teleperformance_case3.Application.Common.Interfaces;

namespace teleperformance_case3.Application.Validators.Commands;

public class DeleteShoppingListValidator : AbstractValidator<DeleteShoppingListCommand>
{
    public DeleteShoppingListValidator(IApplicationDbContext context)
    {
        RuleFor(e => e.Id)
            .GreaterThan(0)
            .MustAsync(async (id, _) =>
            {
                var shoppingList = await context.ShoppingLists
                    .FirstOrDefaultAsync(e => e.IsActive && e.Id == id);

                return shoppingList != null;
            });
    }
}