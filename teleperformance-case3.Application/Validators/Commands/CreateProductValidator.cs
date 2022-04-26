using FluentValidation;
using Microsoft.EntityFrameworkCore;
using teleperformance_case3.Application.Commands;
using teleperformance_case3.Application.Common.Interfaces;

namespace teleperformance_case3.Application.Validators.Commands;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator(IApplicationDbContext context)
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .NotNull()
            .MustAsync(async (name, _) =>
            {
                var product = await context.Products.FirstOrDefaultAsync(e => e.IsActive && e.Name == name);
                return product == null;
            });
        RuleFor(e => e.CategoryId)
            .GreaterThan(0)
            .MustAsync(async (categoryId, _) =>
            {
                var category = await context.Categories.FirstOrDefaultAsync(e => e.IsActive && e.Id == categoryId);
                return category != null;
            });
    }
}