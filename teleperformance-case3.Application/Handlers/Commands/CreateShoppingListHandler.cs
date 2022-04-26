using AutoMapper;
using MediatR;
using teleperformance_case3.Application.Commands;
using teleperformance_case3.Application.Common;
using teleperformance_case3.Application.Common.Interfaces;
using teleperformance_case3.Domain.Entities;

namespace teleperformance_case3.Application.Handlers.Commands;

internal class CreateShoppingListHandler : IRequestHandler<CreateShoppingListCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateShoppingListHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<bool> Handle(CreateShoppingListCommand request, CancellationToken cancellationToken)
    {
        var shoppingList = _mapper.Map<ShoppingList>(request);
        try
        {
            await _context.ShoppingLists.AddAsync(shoppingList, cancellationToken);
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