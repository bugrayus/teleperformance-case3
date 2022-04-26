using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using teleperformance_case3.Application.Commands;
using teleperformance_case3.Application.Common;
using teleperformance_case3.Application.Common.Helpers;
using teleperformance_case3.Application.Common.Interfaces;
using teleperformance_case3.Application.Models;

namespace teleperformance_case3.Application.Handlers.Commands;

internal class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly Token _token;

    public LoginHandler(IApplicationDbContext context, IMapper mapper, Token token)
    {
        _context = context;
        _mapper = mapper;
        _token = token;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        //move to validator
        var user = await _context.Users.FirstOrDefaultAsync(e => e.IsActive && e.Mail == request.Mail,
            cancellationToken);

        if (user == null)
            throw new ApiException(new Error
            {
                Message = $"No user found by mail {request.Mail}"
            });

        var hashedPassword = PasswordHasher.HashPassword(request.Password, user.Salt).Item2;
        var token = _token.GenerateToken(user);
        if (hashedPassword == user.HashedPassword)
            return new LoginResponse
            {
                Token = token
            };

        throw new ApiException(new Error
        {
            Message = $"Wrong password {request.Mail}"
        });
    }
}