using AutoMapper;
using MediatR;
using teleperformance_case3.Application.Commands;
using teleperformance_case3.Application.Common;
using teleperformance_case3.Application.Common.Helpers;
using teleperformance_case3.Application.Common.Interfaces;
using teleperformance_case3.Domain.Entities;

namespace teleperformance_case3.Application.Handlers.Commands;

internal class RegisterHandler : IRequestHandler<RegisterCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public RegisterHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var (salt, hashed) = PasswordHasher.HashPassword(request.Password, null);
        var newUser = _mapper.Map<User>(request);
        newUser.Salt = salt;
        newUser.HashedPassword = hashed;
        try
        {
            await _context.Users.AddAsync(newUser, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            throw new ApiException(new Error
            {
                Message = "Kullanıcı oluşturulurken hata ile karşılaşıldı."
            });
        }

        return true;
    }
}