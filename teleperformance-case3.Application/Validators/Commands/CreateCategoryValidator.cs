using FluentValidation;
using Microsoft.EntityFrameworkCore;
using teleperformance_case3.Application.Commands;
using teleperformance_case3.Application.Common.Interfaces;

namespace teleperformance_case3.Application.Validators.Commands;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryValidator(IApplicationDbContext context)
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .NotNull()
            .MustAsync(async (name, _) =>
            {
                var category = await context.Categories.FirstOrDefaultAsync(e => e.IsActive && e.Name == name);
                return category == null;
            });
    }
}