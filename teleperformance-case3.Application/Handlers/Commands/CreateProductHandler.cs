using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using teleperformance_case3.Application.Commands;
using teleperformance_case3.Application.Common;
using teleperformance_case3.Application.Common.Exceptions;
using teleperformance_case3.Application.Common.Interfaces;
using teleperformance_case3.Domain.Entities;

namespace teleperformance_case3.Application.Handlers.Commands;

internal class CreateProductHandler : IRequestHandler<CreateProductCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _userService;

    public CreateProductHandler(IMapper mapper, IApplicationDbContext context, ICurrentUserService userService)
    {
        _mapper = mapper;
        _context = context;
        _userService = userService;
    }

    public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(e => e.Id == _userService.UserId, cancellationToken);
        if (user != null && user.Role != "Admin")
            throw new UnauthorizedException(new Error
            {
                Message = "Kullanıcı admin değil!"
            });
        var category =
            await _context.Categories.FirstOrDefaultAsync(e => e.IsActive && e.Id == request.CategoryId,
                cancellationToken);
        var product = _mapper.Map<Product>(request);
        product.Category = category;
        try
        {
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new ApiException(new Error
            {
                Message = "Veritabanına kaydederken bir hata oluştu.",
                Errors = new List<string> {ex.Message}
            });
        }

        return true;
    }
}