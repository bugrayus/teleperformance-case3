using MediatR;
using Microsoft.EntityFrameworkCore;
using teleperformance_case3.Application.Commands;
using teleperformance_case3.Application.Common;
using teleperformance_case3.Application.Common.Exceptions;
using teleperformance_case3.Application.Common.Interfaces;

namespace teleperformance_case3.Application.Handlers.Commands;

internal class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _userService;

    public DeleteProductHandler(IApplicationDbContext context, ICurrentUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var user =
            await _context.Users
                .FirstOrDefaultAsync(e => e.Id == _userService.UserId, cancellationToken);
        if (user != null && user.Role != "Admin")
            throw new UnauthorizedException(new Error
            {
                Message = "Kullanıcı admin değil!"
            });
        var product =
            await _context.Products.FirstOrDefaultAsync(e => e.IsActive && e.Id == request.Id, cancellationToken);

        try
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new ApiException(new Error
            {
                Message = "Veritabanından silerken bir hata oluştu.",
                Errors = new List<string> {ex.Message}
            });
        }

        return true;
    }
}