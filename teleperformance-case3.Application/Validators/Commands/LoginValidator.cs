using FluentValidation;
using teleperformance_case3.Application.Commands;

namespace teleperformance_case3.Application.Validators.Commands;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(e => e.Mail)
            .NotEmpty()
            .NotNull()
            .EmailAddress();
        RuleFor(e => e.Password)
            .NotEmpty()
            .NotNull()
            .MinimumLength(8)
            .Matches(@"[A-Z]+").WithMessage("Şifre en az bir büyük karakter içermelidir.")
            .Matches(@"[a-z]+").WithMessage("Şifre en az bir küçük karakter içermelidir.")
            .Matches(@"[0-9]+").WithMessage("Şifre en az bir rakam içermelidir.");
    }
}