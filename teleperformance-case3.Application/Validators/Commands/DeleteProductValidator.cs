using FluentValidation;
using Microsoft.EntityFrameworkCore;
using teleperformance_case3.Application.Commands;
using teleperformance_case3.Application.Common.Interfaces;

namespace teleperformance_case3.Application.Validators.Commands;

public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductValidator(IApplicationDbContext context)
    {
        RuleFor(e => e.Id)
            .GreaterThan(0)
            .MustAsync(async (id, _) =>
            {
                var product = await context.Products
                    .FirstOrDefaultAsync(e => e.Id == id && e.IsActive);

                return product != null;
            });
    }
}