using FluentValidation;
using Microsoft.EntityFrameworkCore;
using teleperformance_case3.Application.Commands;
using teleperformance_case3.Application.Common.Interfaces;

namespace teleperformance_case3.Application.Validators.Commands;

public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    private readonly IApplicationDbContext _context;

    public RegisterValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(e => e.Mail)
            .NotEmpty()
            .NotNull()
            .EmailAddress()
            .MustAsync(async (mail, _) =>
            {
                var user = await _context.Users.FirstOrDefaultAsync(e => e.Mail == mail);
                return user == null;
            }).WithMessage("Kullanıcı zaten var.");
        RuleFor(e => e.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(e => e.Surname)
            .NotEmpty()
            .NotNull();
        RuleFor(e => e.Password)
            .NotEmpty()
            .NotNull()
            .MinimumLength(8)
            .Matches(@"[A-Z]+").WithMessage("Şifre en az bir büyük karakter içermelidir.")
            .Matches(@"[a-z]+").WithMessage("Şifre en az bir küçük karakter içermelidir.")
            .Matches(@"[0-9]+").WithMessage("Şifre en az bir rakam içermelidir.");
    }
}