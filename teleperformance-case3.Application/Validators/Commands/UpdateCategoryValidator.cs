using FluentValidation;
using Microsoft.EntityFrameworkCore;
using teleperformance_case3.Application.Commands;
using teleperformance_case3.Application.Common.Interfaces;

namespace teleperformance_case3.Application.Validators.Commands;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryValidator(IApplicationDbContext context)
    {
        RuleFor(e => e.Id)
            .GreaterThan(0)
            .MustAsync(async (id, _) =>
            {
                var category = await context.Categories
                    .FirstOrDefaultAsync(e => e.IsActive && e.Id == id);
                return category != null;
            });
        RuleFor(e => e.Name)
            .NotNull()
            .NotEmpty();
    }
}