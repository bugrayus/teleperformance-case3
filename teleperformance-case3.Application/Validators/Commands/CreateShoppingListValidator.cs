using FluentValidation;
using teleperformance_case3.Application.Commands;

namespace teleperformance_case3.Application.Validators.Commands;

public class CreateShoppingListValidator : AbstractValidator<CreateShoppingListCommand>
{
    public CreateShoppingListValidator()
    {
        RuleFor(e => e.Name)
            .NotNull()
            .NotEmpty();
    }
}